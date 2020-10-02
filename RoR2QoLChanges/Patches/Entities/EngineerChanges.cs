using BepInEx.Logging;

using HarmonyLib;

using RoR2;

using RoR2QoLChanges.Configuration;
using RoR2QoLChanges.Configuration.Survivors;

using RoR2QoLChanges.Patches;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Patches.Entities
{
    public class EngineerChanges : MonoModPatchable
    {
        protected static EngineerConfig activeConfig;

        public EngineerChanges(EngineerConfig config) => activeConfig = config;

        public override void ApplyPatches()
        {
            if (!activeConfig.Enabled.Value)
                return;

            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;

            On.EntityStates.Engi.EngiMissilePainter.Paint.OnEnter += Paint_OnEnter;
        }

        private void Paint_OnEnter(On.EntityStates.Engi.EngiMissilePainter.Paint.orig_OnEnter orig, EntityStates.Engi.EngiMissilePainter.Paint self)
        {
            orig(self);
            EngiPaint_Post_OnEnter(self);
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);
            Post_CharacterBody_RecalculateStats(self);
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
    }
}
