// FeatherCapeJump.cs – BepInEx plugin to restore the +20 % jump bonus when wearing a Feather Cape in Valheim (Hildir + Ashlands builds)
// ------------------------------------------------------------------------------------
// Build against:
//   • BepInEx 5.4.x                  (core/BepInEx.dll)
//   • Harmony 2.x                   (core/0Harmony.dll)
//   • Game assemblies               (assembly_valheim.dll, UnityEngine DLLs)
// Target framework: net472

using BepInEx;
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
        private const string ModGuid = "shivam.valheim.feathercapejump";
        private const string ModName = "Feather Cape Jump Restore";
        private const string ModVersion = "1.1.0";

        // 20 % higher jump → multiply by 1.2 one time per jump
        private const float JumpMultiplier = 1.20f;

        private static readonly Harmony Harmony = new Harmony(ModGuid);

        private void Awake()
        {
            Harmony.PatchAll();
            Logger.LogInfo($"{ModName} {ModVersion} loaded");
        }

        private void OnDestroy() => Harmony.UnpatchSelf();

        // ---------------------------------------------------------------------------
        //  Helpers
        // ---------------------------------------------------------------------------
        private static bool WearingFeatherCape(Player player)
        {
            if (player == null) return false;

            // Build list of equipped items
            List<ItemDrop.ItemData> worn = ListPool<ItemDrop.ItemData>.Get(); // Changed from Create() to Get()
            player.GetInventory().GetWornItems(worn);

            bool result = worn.Any(i => i != null && i.m_shared != null && i.m_shared.m_name == "$item_cape_feather");

            ListPool<ItemDrop.ItemData>.Release(worn);
            return result;
        }

        // ---------------------------------------------------------------------------
        //  Harmony patch: Character.Jump (vanilla method sets body velocity.y = m_jumpForce)
        // ---------------------------------------------------------------------------
        [HarmonyPatch(typeof(Character), nameof(Character.Jump))] // Changed Player to Character
        private static class Player_Jump_Patch // You can keep this class name, or rename it to Character_Jump_Patch for clarity
        {
            // Store original value so we can restore and avoid cumulative stacking
            private static void Prefix(Character __instance, ref float ___m_jumpForce, out float __state) // Changed Player to Character for __instance
            {
                __state = ___m_jumpForce; // keep pristine copy
                // We need to check if the Character instance is actually the player
                // and then if that player is wearing the feather cape.
                if (__instance is Player playerInstance && WearingFeatherCape(playerInstance))
                {
                    ___m_jumpForce *= JumpMultiplier;
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