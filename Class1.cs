// BuoyantFeatherCape.cs – BepInEx plugin to restore the +20 % jump bonus when wearing a Feather Cape in Valheim (Hildir + Ashlands builds)
// ------------------------------------------------------------------------------------
// Build against:
//   • BepInEx 5.4.x                  (core/BepInEx.dll)
//   • Harmony 2.x                   (core/0Harmony.dll)
//   • Game assemblies               (assembly_valheim.dll, UnityEngine DLLs)
// Target framework: net472

using BepInEx;
using BepInEx.Configuration; // Added for config file handling
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool; // Ensure this using directive is present

namespace FeatherCapeJumpRestore
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class BuoyantFeatherCape : BaseUnityPlugin
    {        private const string ModGuid = "BuoyantFeatherCape.Official";
        private const string ModName = "Feather Cape Jump & Speed & Eitr"; // Updated ModName
        private const string ModVersion = "1.3.0"; // Incremented version for new feature

        // Configuration entries - made nullable
        private static ConfigEntry<float>? _jumpHeightMultiplierConfig;
        private static ConfigEntry<float>? _runSpeedMultiplierConfig; // New config for run speed
        private static ConfigEntry<float>? _eitrRegenMultiplierConfig; // New config for eitr regen

        // Default multipliers if config is invalid or not found initially
        private const float DefaultJumpMultiplier = 1.20f; 
        private const float DefaultRunSpeedMultiplier = 1.0f; // Default run speed multiplier (no change)
        private const float DefaultEitrRegenMultiplier = 1.0f; // Default eitr regen multiplier (no change)

        private static readonly Harmony Harmony = new Harmony(ModGuid);

        private void Awake()
        {            // Initialize configuration for Jump Height
            _jumpHeightMultiplierConfig = Config.Bind<float>(
                "Jump Settings",                    // Section name
                "JumpHeightMultiplier",             // Key name
                DefaultJumpMultiplier,              // Default value
                new ConfigDescription("The multiplier for jump height when wearing the Feather Cape. E.g., 1.20 means 20% higher jump.", 
                                    new AcceptableValueRange<float>(1.0f, 10.0f)));

            // Initialize configuration for Run Speed
            _runSpeedMultiplierConfig = Config.Bind<float>(
                "Speed Settings",                   // Section name
                "RunSpeedMultiplier",               // Key name
                DefaultRunSpeedMultiplier,          // Default value
                new ConfigDescription("The multiplier for run speed when wearing the Feather Cape. E.g., 1.2 means 20% faster run speed. Default is 1.0 (no change).",
                                    new AcceptableValueRange<float>(1.0f, 3.0f))); // Range from 1.0x to 3.0x// Initialize configuration for Eitr Regen
            _eitrRegenMultiplierConfig = Config.Bind<float>(
                "Eitr Settings",                    // Section name - separate section for visibility
                "EitrRegenMultiplier",              // Key name
                DefaultEitrRegenMultiplier,         // Default value
                new ConfigDescription("The multiplier for Eitr regeneration when wearing the Feather Cape. E.g., 2.0 means 2x faster Eitr regen. Default is 1.0 (no change).",
                                    new AcceptableValueRange<float>(1.0f, 5.0f))); // Range from 1.0x to 5.0x

            Harmony.PatchAll();
            Logger.LogInfo($"{ModName} {ModVersion} loaded. Jump multiplier: {(_jumpHeightMultiplierConfig != null ? _jumpHeightMultiplierConfig.Value : DefaultJumpMultiplier)}, Run Speed multiplier: {(_runSpeedMultiplierConfig != null ? _runSpeedMultiplierConfig.Value : DefaultRunSpeedMultiplier)}, Eitr Regen multiplier: {(_eitrRegenMultiplierConfig != null ? _eitrRegenMultiplierConfig.Value : DefaultEitrRegenMultiplier)}");
        }

        private void OnDestroy() => Harmony.UnpatchSelf();

        // ---------------------------------------------------------------------------
        //  Helpers
        // ---------------------------------------------------------------------------
        private static bool WearingFeatherCape(Player player)
        {
            if (player == null) return false;

            // List<ItemDrop.ItemData> worn = ListPool<ItemDrop.ItemData>.Get(); // Ensured correct type
            // player.GetInventory().GetWornItems(worn);

            // bool result = worn.Any(i => i != null && i.m_shared != null && i.m_shared.m_name == "$item_cape_feather");

            // ListPool<ItemDrop.ItemData>.Release(worn); // Ensured correct type
            // return result;

            return player.GetInventory()
                 .GetAllItems()
                 .Any(i => i.m_equipped &&
                           i.m_shared != null &&
                           i.m_shared.m_name == "$item_cape_feather");
        }

        // ---------------------------------------------------------------------------
        //  Harmony patch: Character.Jump (vanilla method sets body velocity.y = m_jumpForce)
        // ---------------------------------------------------------------------------
        [HarmonyPatch(typeof(Character), nameof(Character.Jump))] 
        private static class Player_Jump_Patch 
        {
            // Store original value so we can restore and avoid cumulative stacking
            private static void Prefix(Character __instance, ref float ___m_jumpForce, out float __state) 
            {
                __state = ___m_jumpForce; 
                if (__instance is Player playerInstance && WearingFeatherCape(playerInstance))
                {
                    // Use the configured value, with a fallback to default if somehow null
                    ___m_jumpForce *= (_jumpHeightMultiplierConfig != null ? _jumpHeightMultiplierConfig.Value : DefaultJumpMultiplier);
                }
            }

            // Restore after original method executes
            private static void Postfix(ref float ___m_jumpForce, float __state)
            {
                ___m_jumpForce = __state;
            }
        }

        // ---------------------------------------------------------------------------
        //  Harmony patch: Player.GetRunSpeedFactor
        // ---------------------------------------------------------------------------
        [HarmonyPatch(typeof(Player), "GetRunSpeedFactor")] // Changed nameof to string literal
        private static class Player_GetRunSpeedFactor_Patch
        {
            private static void Postfix(Player __instance, ref float __result)
            {
                if (WearingFeatherCape(__instance))
                {
                    // Apply the configured run speed multiplier
                    // Use a fallback to default if config is somehow null
                    __result *= (_runSpeedMultiplierConfig != null ? _runSpeedMultiplierConfig.Value : DefaultRunSpeedMultiplier);
                }
            }
        }

        // ---------------------------------------------------------------------------
        //  Harmony patch: Player.UpdateStats (for Eitr regeneration enhancement)
        // ---------------------------------------------------------------------------
        [HarmonyPatch(typeof(Player), "UpdateStats")]
        private static class Player_UpdateStats_Patch
        {
            // Store original value so we can restore and avoid cumulative stacking
            private static void Prefix(Player __instance, ref float ___m_eiterRegen, out float __state)
            {
                __state = ___m_eiterRegen;
                if (WearingFeatherCape(__instance))
                {
                    // Use the configured value, with a fallback to default if somehow null
                    ___m_eiterRegen *= (_eitrRegenMultiplierConfig != null ? _eitrRegenMultiplierConfig.Value : DefaultEitrRegenMultiplier);
                }
            }

            // Restore after original method executes
            private static void Postfix(ref float ___m_eiterRegen, float __state)
            {
                ___m_eiterRegen = __state;
            }
        }
    }
}