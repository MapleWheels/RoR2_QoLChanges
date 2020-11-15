using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using RoR2;

using RoR2QoLRewrite.Configuration.Survivors;

using UnityEngine;

namespace RoR2QoLRewrite.Modules
{
    class CaptainModule : IModule
    {
        private static CaptainConfig Config;
        private static ManualLogSource Logger;
        private BuffWard HpBoostWard = null;

        public bool IsEnabled { get; private set; }
        public bool IsLoaded { get; private set; }

        public void DisableModule()
        {
            if (!IsEnabled)
                return;
            CaptainPatch.Unpatch();
            IsEnabled = false;
        }

        public void EnableModule()
        {
            if (IsEnabled || !IsLoaded)
                return;
            CaptainPatch.Patch();
            IsEnabled = true;
        }

        public void LoadModule(ConfigFile file, ManualLogSource logger)
        {
            if (IsLoaded)
                return;
            Config = file.BindModel<CaptainConfig>(logger);
            EntityPatch();

            CaptainPatch.MissingHpHealingBuffRatio = Config.MissingHpHealingBoostBuff_PercentMissingHpRatio;
            CaptainPatch.Beacon_HealingRadiusBase = Config.Beacon_HealingDefaultRadius;
            CaptainPatch.Beacon_HealingRadiusPerLevel = Config.Beacon_HealingRadiusIncreasePerLevel;
            CaptainPatch.Beacon_MaxHpHealingBase = Config.Beacon_MaxHpHealingBase;
            CaptainPatch.Beacon_MaxHpHealingRatioPerLevel = Config.Beacon_MaxHpHealingRatioPerLevel;

            IsLoaded = true;

            if (Config.Enabled)
                EnableModule();
        }

        public void SetConfig(ConfigFile file)
        {
            if (!IsLoaded)
                return;
            Config.SetConfigFile(file);
        }

        public void UnloadModule()
        {
            if (!IsLoaded)
                return;
            if (IsEnabled)
                DisableModule();

            EntityUnpatch();

            IsLoaded = false;
        }

        private void EntityPatch()
        {
            Buffs.MissingHpHealingBoostBuff buff = (Buffs.MissingHpHealingBoostBuff)PluginCoreModule.BuffCatalog[nameof(Buffs.MissingHpHealingBoostBuff)];

            if (buff == null)
            {
                buff = new Buffs.MissingHpHealingBoostBuff();
                buff.Init();
                PluginCoreModule.BuffCatalog[nameof(Buffs.MissingHpHealingBoostBuff)] = buff;
            }

            GameObject healZonePrefab = EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab;

            if (healZonePrefab)
            {
                HpBoostWard = healZonePrefab.GetComponent<BuffWard>();
                if (!HpBoostWard)
                    HpBoostWard = healZonePrefab.AddComponent<BuffWard>();
                PluginCoreModule.PrefabCache.Add(nameof(EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab), healZonePrefab);
            }

            if (!HpBoostWard)
            {
                Logger.LogError("CaptainModule::EntityPatch() | Unable to add BuffWard for Captain HealZone.");
                return;
            }

            //Defaults
            HpBoostWard.buffType = PluginCoreModule.BuffCatalog[nameof(Buffs.MissingHpHealingBoostBuff)].BuffDef.buffIndex;
            HpBoostWard.teamFilter = new TeamFilter();
            HpBoostWard.teamFilter.teamIndex = TeamIndex.Player;
            HpBoostWard.radius = 10f;
            HpBoostWard.buffDuration = 0.25f;
            HpBoostWard.buffTimer = 0.25f;
            HpBoostWard.Networkradius = 10f;
        }

        private void EntityUnpatch()
        {
            if (!HpBoostWard)
                return;

            GameObject.Destroy(HpBoostWard);
            HpBoostWard = null;
        }

        static class CaptainPatch
        {
            static bool LOADED = false;
            internal static float MissingHpHealingBuffRatio = 0.5f;
            internal static float Beacon_MaxHpHealingBase = 0.1f;
            internal static float Beacon_MaxHpHealingRatioPerLevel = 0.01f;
            internal static float Beacon_HealingRadiusBase = 10f;
            internal static float Beacon_HealingRadiusPerLevel = 1f;

            internal static void Patch() 
            {
                if (LOADED)
                    Unpatch();

                Patches.Ror2_HealthComponent.Pre_Heal += ApplyMissingHpBuffHealing;
                Patches.Ror2_CharacterBody.Pre_RecalculateStats += UpdateHealWardZoneStats;

                LOADED = true;
            }
            
            internal static void Unpatch() 
            {
                Patches.Ror2_HealthComponent.Pre_Heal -= ApplyMissingHpBuffHealing;
                Patches.Ror2_CharacterBody.Pre_RecalculateStats -= UpdateHealWardZoneStats;

                LOADED = false;
            }

            static void UpdateHealWardZoneStats(RoR2.CharacterBody self)
            {
                if (!self.CompareTag("Player"))
                    return;

                HealingWard ward = EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab.GetComponent<HealingWard>();

                if (ward)
                {
                    ward.healFraction = (Beacon_MaxHpHealingBase + Beacon_MaxHpHealingRatioPerLevel * self.level) / 100f * ward.interval;
                    ward.radius = Beacon_HealingRadiusBase + Beacon_HealingRadiusPerLevel * self.level;
                }
            }

            static float ApplyMissingHpBuffHealing(RoR2.HealthComponent self, float amount, RoR2.ProcChainMask procChainMask, bool nonRegen)
            {
                CharacterBody component = self.GetComponent<CharacterBody>();
                Buffs.MissingHpHealingBoostBuff buff = (Buffs.MissingHpHealingBoostBuff)PluginCoreModule.BuffCatalog[nameof(Buffs.MissingHpHealingBoostBuff)];

                if (component && buff != null && component.HasBuff(buff.BuffDef.buffIndex))
                    amount = GetAdjustedHealAmount(amount, self.health, self.fullHealth);

                return amount;
            }

            static float GetAdjustedHealAmount(float healAmount, float currentHp, float maxHp) => healAmount * (1 + ((1 - currentHp / maxHp) * MissingHpHealingBuffRatio));
        }
    }
}
