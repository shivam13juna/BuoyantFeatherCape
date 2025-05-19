// FeatherCapeJump.cs – BepInEx plugin to restore the +20 % jump bonus when wearing a Feather Cape in Valheim (Hildir + Ashlands builds)
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
    public class FeatherCapeJump : BaseUnityPlugin
    {
        private const string ModGuid = "FeatherCapeJump.Official"; 
        private const string ModName = "Feather Cape Jump Restore";
        private const string ModVersion = "1.1.3"; // Incremented version for config cap change

        // Configuration entry - made nullable
        private static ConfigEntry<float>? _jumpHeightMultiplierConfig;

        // Default jump multiplier if config is invalid or not found initially
        private const float DefaultJumpMultiplier = 1.20f; 

        private static readonly Harmony Harmony = new Harmony(ModGuid);

        private void Awake()
        {
            // Initialize configuration
            _jumpHeightMultiplierConfig = Config.Bind<float>(
                "General",                          // Section name
                "JumpHeightMultiplier",             // Key name
                DefaultJumpMultiplier,              // Default value
                new ConfigDescription("The multiplier for jump height when wearing the Feather Cape. E.g., 1.20 means 20% higher jump.", 
                                    new AcceptableValueRange<float>(1.0f, 10.0f))); // Changed cap from 5.0f to 20.0f

            Harmony.PatchAll();
            Logger.LogInfo($"{ModName} {ModVersion} loaded. Jump multiplier set to: {(_jumpHeightMultiplierConfig != null ? _jumpHeightMultiplierConfig.Value : DefaultJumpMultiplier)}");
        }

        private void OnDestroy() => Harmony.UnpatchSelf();

        // ---------------------------------------------------------------------------
        //  Helpers
        // ---------------------------------------------------------------------------
        private static bool WearingFeatherCape(Player player)
        {
            if (player == null) return false;

            List<ItemDrop.ItemData> worn = ListPool<ItemDrop.ItemData>.Get(); // Ensured correct type
            player.GetInventory().GetWornItems(worn);

            bool result = worn.Any(i => i != null && i.m_shared != null && i.m_shared.m_name == "$item_cape_feather");

            ListPool<ItemDrop.ItemData>.Release(worn); // Ensured correct type
            return result;
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
    }
}