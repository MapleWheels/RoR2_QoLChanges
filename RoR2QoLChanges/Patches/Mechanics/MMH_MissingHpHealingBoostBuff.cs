﻿using RoR2;
using RoR2QoLChanges.Additions.Buffs;
using RoR2QoLChanges.Additions.Mechanics;


namespace RoR2QoLChanges.Patches.Mechanics
{
    public class MMH_MissingHpHealingBoostBuff : MonoModPatchable
    {
        public static Configuration.Survivors.CaptainConfig ActiveConfig;

        public MMH_MissingHpHealingBoostBuff(Configuration.Survivors.CaptainConfig config) => ActiveConfig = config;

        public override void ApplyPatches()
        {
            On.RoR2.HealthComponent.Heal += PreHealBuffApply;
            On.EntityStates.CaptainSupplyDrop.HealZoneMainState.OnEnter += ApplyHealingRadiusChanges;
        }

        private void ApplyHealingRadiusChanges(On.EntityStates.CaptainSupplyDrop.HealZoneMainState.orig_OnEnter orig, EntityStates.CaptainSupplyDrop.HealZoneMainState self)
        {
            MissingHpHealingBoostBehaviour component = EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab.GetComponent<MissingHpHealingBoostBehaviour>();

            if (component)
            {
                CharacterBody body = component.GetComponent<CharacterBody>();
                if (body)
                {
                    //calculate healing fraction
                    float teamLevel = body.level;
                    float interval = MissingHpHealingBoostBehaviour.interval;
                    float healFraction = (ActiveConfig.Beacon_MaxHpHealingBase.Value + ActiveConfig.Beacon_MaxHpHealingRatioPerLevel.Value * teamLevel) * interval;

                    //calculate radius
                    float radius = ActiveConfig.Beacon_HealingDefaultRadius.Value + ActiveConfig.Beacon_HealingRadiusIncreasePerLevel.Value * teamLevel;

                    component.SyncHealingStats(false); //get current healingward stats

                    //set the values
                    MissingHpHealingBoostBehaviour.healFraction = healFraction;
                    MissingHpHealingBoostBehaviour.radius = radius;

                    //call the sync method
                    component.SyncHealingStats(true);   //Update the healing ward stats
                }
            }
        }

        private float PreHealBuffApply(On.RoR2.HealthComponent.orig_Heal orig, RoR2.HealthComponent self, float amount, RoR2.ProcChainMask procChainMask, bool nonRegen)
        {
            CharacterBody component = self.GetComponent<CharacterBody>();
            MissingHpHealingBoostBuff buff = (MissingHpHealingBoostBuff)PluginCore.buffCatalog[nameof(MissingHpHealingBoostBuff)];

            UnityEngine.Debug.LogWarning($"MMH_MissingHp..Buff::PreHealBuffApply() | Before={amount}");
            if (component && buff != null)
                if (component.HasBuff(buff.BuffDef.buffIndex))
                    amount = GetAdjustedHealAmount(amount, self.health, self.fullHealth);

            UnityEngine.Debug.LogWarning($"MMH_MissingHp..Buff::PreHealBuffApply() | After={amount}");

            return orig(self, amount, procChainMask, nonRegen);
        }

        public override void RemovePatches()
        {
            On.RoR2.HealthComponent.Heal -= PreHealBuffApply;
            On.EntityStates.CaptainSupplyDrop.HealZoneMainState.OnEnter -= ApplyHealingRadiusChanges;
        }

        public static float GetAdjustedHealAmount(float healAmount, float currentHp, float maxHp) =>
    healAmount * (1 + ((1 - currentHp / maxHp) * ActiveConfig.MissingHpHealingBoostBuff_PercentMissingHpRatio.Value)
    );
    }
}
