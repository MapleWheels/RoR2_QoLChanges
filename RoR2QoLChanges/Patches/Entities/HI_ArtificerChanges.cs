﻿using HarmonyLib;

using RoR2;

using RoR2QoLChanges.Configuration;
using RoR2QoLChanges.Configuration.Survivors;

using RoR2QoLChanges.Patches;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2QoLChanges.Patches.Entities
{
    public class HI_ArtificerChanges : HarmonyPatchable
    {
        protected static ArtificerConfig artificerConfig;

        public HI_ArtificerChanges(ArtificerConfig activeConfig, Harmony instance) : base(instance)
        {
            artificerConfig = activeConfig;
        }

        public override void ApplyPatches()
        {
            harmonyInstance.Patch(
                original: MI_CharacterBody_RecalculateStats,
                postfix: new HarmonyMethod(MI_Post_RecalculateStats)
                );
        }

        /// <summary>
        /// Increases the M1/Fire Bolt max charges on Artificer/Mage.
        /// </summary>
        /// <param name="__instance"></param>
        public static void Post_CharacterBody_RecalculateStats(RoR2.CharacterBody __instance)
        {
            if (__instance.name.ToLower().Contains(ArtificerConfig.ArtificerBodyName.ToLower()))
            {
                GenericSkill m1 = __instance.skillLocator.primary;

                m1.SetBonusStockFromBody(
                    Math.Min(
                        (int)(__instance.level/artificerConfig.M1Fire_LevelsPerCharge.Value),
                        artificerConfig.M1Fire_MaxLevelCharges.Value
                        ) +
                    Math.Min(
                        (__instance.inventory.GetItemCount(ItemIndex.SecondarySkillMagazine)/artificerConfig.M1Fire_BackupMagsPerCharge.Value),
                        artificerConfig.M1Fire_MaxBackupMagCharges.Value
                        )
                    );

                m1.cooldownScale *= 1f - Mathf.Min(
                    Mathf.Max(
                        __instance.attackSpeed - (__instance.baseAttackSpeed + __instance.levelAttackSpeed * __instance.level), 0f) 
                    * artificerConfig.M1Fire_CooldownPerAttackSpeedRatio.Value,
                    1f - artificerConfig.M1Fire_MaxCooldownFromAttackSpeed.Value
                    );
            }
        }

        protected static MethodInfo MI_CharacterBody_RecalculateStats;
        protected static MethodInfo MI_Post_RecalculateStats;

        static HI_ArtificerChanges()
        {
            MI_CharacterBody_RecalculateStats = typeof(CharacterBody).GetMethod(nameof(CharacterBody.RecalculateStats));
            MI_Post_RecalculateStats = typeof(HI_ArtificerChanges).GetMethod(nameof(Post_CharacterBody_RecalculateStats));
        }
    }
}
