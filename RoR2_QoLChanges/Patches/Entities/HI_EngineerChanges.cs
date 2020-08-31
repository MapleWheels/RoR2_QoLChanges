using BepInEx.Logging;

using HarmonyLib;

using RoR2;

using RoR2_QoLChanges.Configuration;
using RoR2_QoLChanges.Configuration.Survivors;

using RoR2QoLChanges.Patches;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoR2_QoLChanges.Patches.Entities
{
    public class HI_EngineerChanges : HarmonyPatchable
    {
        protected static EngineerConfig activeConfig;

        public HI_EngineerChanges(EngineerConfig config, Harmony instance) : base(instance) => activeConfig = config;

        public override void ApplyPatches()
        {
            harmonyInstance.Patch(
                original: HI_EngineerChanges.MI_CharacterBody_RecalculateStats,
                postfix: HI_EngineerChanges.MI_Post_RecalculateStats
                );

            harmonyInstance.Patch(
                original: HI_EngineerChanges.MI_EngiPaint_OnEnter,
                prefix: HI_EngineerChanges.MI_EngiPaint_Post_OnEnter
                );
        }

        public static void Post_CharacterBody_RecalculateStats(RoR2.CharacterBody __instance)
        {
            if (__instance.name.ToLower().Trim().Contains(EngineerConfig.EngiCarbonizerTurretBodyName.ToLower()))
            {
                PropertyInfo piMoveSpeed = AccessTools.DeclaredProperty(__instance.GetType(), nameof(CharacterBody.moveSpeed));
                float movespeed = (float)piMoveSpeed.GetValue(__instance);

                movespeed *= __instance.sprintingSpeedMultiplier;

                piMoveSpeed.SetValue(__instance, movespeed);
            }
        }

        public static void EngiPaint_Post_OnEnter(EntityStates.Engi.EngiMissilePainter.Paint __instance)
        {
            EntityStates.Engi.EngiMissilePainter.Paint.maxDistance = activeConfig.EngiMissilePainer_MaxDistance.Value;   
        }

        protected static MethodInfo MI_CharacterBody_RecalculateStats;
        protected static HarmonyMethod MI_Post_RecalculateStats;

        protected static MethodInfo MI_EngiPaint_OnEnter;
        protected static HarmonyMethod MI_EngiPaint_Post_OnEnter;

        static HI_EngineerChanges()
        {
            MI_CharacterBody_RecalculateStats = typeof(CharacterBody).GetMethod(nameof(CharacterBody.RecalculateStats));
            MI_Post_RecalculateStats = new HarmonyMethod(typeof(HI_EngineerChanges).GetMethod(nameof(Post_CharacterBody_RecalculateStats)));

            MI_EngiPaint_OnEnter = typeof(EntityStates.Engi.EngiMissilePainter.Paint).GetMethod(nameof(EntityStates.Engi.EngiMissilePainter.Paint.OnEnter));
            MI_EngiPaint_Post_OnEnter = new HarmonyMethod(typeof(HI_EngineerChanges).GetMethod(nameof(EngiPaint_Post_OnEnter)));
        }
    }
}
