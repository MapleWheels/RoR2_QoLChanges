using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx.Extensions.Configuration;
using RoR2;
using RoR2QoLRewrite.Buffs;
using RoR2QoLRewrite.Configuration.Survivors;
using RoR2QoLRewrite.Util;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2QoLRewrite.Modules
{
    internal class CaptainModule : ModuleBase
    {
        private const string HealZonePrefabName = nameof(EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab);

        protected override void OnDisable()
        {
            On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.OnEnter -= ShockZoneMainState_OnEnter;
            On.EntityStates.CaptainSupplyDrop.HealZoneMainState.OnEnter -= HealZoneMainState_OnEnter;

            HealingWard component = EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab.GetComponent<HealingWard>();
            if (component)
            {
                //Reset to vanilla
                component.radius = 10f;
                component.healFraction = 0.025f; //Value = 0.1 * interval_rate(0.25)
            }

            GameObject.Destroy(Cache<BuffWard>.Dispose(HealZonePrefabName));

            EntityStates.CaptainSupplyDrop.ShockZoneMainState.shockRadius = 10f;
        }

        protected override void OnEnable()
        {
            On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.OnEnter += ShockZoneMainState_OnEnter;
            On.EntityStates.CaptainSupplyDrop.HealZoneMainState.OnEnter += HealZoneMainState_OnEnter;

            BuffWard buffWard = Cache<BuffWard>.Get(HealZonePrefabName);
            if (!buffWard)
            {
                buffWard = Cache<GameObject>.Get(HealZonePrefabName).AddComponent<BuffWard>();
                Cache<BuffWard>.Add(HealZonePrefabName, buffWard);

                buffWard.buffType = Cache<BuffIndex>.Get(nameof(MissingHpHealingBoostBuff));
                buffWard.buffDuration = 0.25f;
            }
        }

        private void HealZoneMainState_OnEnter(On.EntityStates.CaptainSupplyDrop.HealZoneMainState.orig_OnEnter orig, EntityStates.CaptainSupplyDrop.HealZoneMainState self)
        {
            float level = LocalUserManager.GetFirstLocalUser().cachedBody.level;

            HealingWard component = Cache<HealingWard>.Get(HealZonePrefabName);
            if (component)
            {
                component.radius = CaptainConfig.Beacon_HealingDefaultRadius + CaptainConfig.Beacon_HealingRadiusIncreasePerLevel * level;
                component.healFraction = CaptainConfig.Beacon_MaxHpHealingBase + CaptainConfig.Beacon_MaxHpHealingRatioPerLevel * level;
            }

            BuffWard buffWard = Cache<BuffWard>.Get(HealZonePrefabName);
            if (buffWard)
            {
                buffWard.radius = CaptainConfig.Beacon_HealingDefaultRadius + CaptainConfig.Beacon_HealingRadiusIncreasePerLevel * level;
            }

            orig(self);
        }

        private void ShockZoneMainState_OnEnter(On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.orig_OnEnter orig, EntityStates.CaptainSupplyDrop.ShockZoneMainState self)
        {
            float level = LocalUserManager.GetFirstLocalUser().cachedBody.level;

            EntityStates.CaptainSupplyDrop.ShockZoneMainState.shockRadius = CaptainConfig.Beacon_ShockDefaultRadius + CaptainConfig.Beacon_ShockRadiusIncreasePerLevel * level;

            orig(self);
        }

        protected override void OnLoad()
        {
            Cache<GameObject>.Add(HealZonePrefabName, EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab);
            Cache<HealingWard>.Add(HealZonePrefabName, EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab.GetComponent<HealingWard>());
        }

        protected override void OnUnload()
        {
            Cache<HealingWard>.Dispose(HealZonePrefabName);
            Cache<GameObject>.Dispose(HealZonePrefabName);
        }
    }
}
