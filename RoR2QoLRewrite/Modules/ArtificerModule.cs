using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;
using RoR2QoLRewrite.Configuration.Survivors;

using RoR2;
using UnityEngine;

using System;

namespace RoR2QoLRewrite.Modules
{
    class ArtificerModule : IModule
    {
        private static ArtificerConfig Config;
        public bool IsEnabled { get; private set; }
        public bool IsLoaded { get; private set; }

        public void DisableModule()
        {
            if (!IsEnabled)
                return;
            ArtificerPatch.Unpatch();
            IsEnabled = false;
        }

        public void EnableModule()
        {
            if (IsEnabled || !IsLoaded)
                return;
            ArtificerPatch.Patch();
            IsEnabled = true;
        }

        public void LoadModule(ConfigFile file, ManualLogSource logger)
        {
            if (IsLoaded)
                return;
            Config = file.BindModel<ArtificerConfig>(logger);

            ArtificerPatch.M1_LevelsPerCharge = Config.M1Fire_LevelsPerCharge;
            ArtificerPatch.M1_MaxLevelCharges = Config.M1Fire_MaxLevelCharges;
            ArtificerPatch.M1_SkillMagsPerCharge = Config.M1Fire_BackupMagsPerCharge;
            ArtificerPatch.M1_MaxSkillMagCharges = Config.M1Fire_MaxBackupMagCharges;
            ArtificerPatch.M1_CooldownPerAttackSpeedRatio = Config.M1Fire_CooldownPerAttackSpeedRatio;
            ArtificerPatch.M1_MaxCDFromAttackSpeed = Config.M1Fire_MaxCooldownFromAttackSpeed; 

            IsLoaded = true;
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
            IsLoaded = false;
        }

        static class ArtificerPatch
        {
            static bool PATCH_ACTIVE = false;
            internal static string ArtificerBodyName="Mage";
            internal static int
                M1_LevelsPerCharge=10,
                M1_MaxLevelCharges=0,
                M1_SkillMagsPerCharge=9999,
                M1_MaxSkillMagCharges=0;

            internal static float
                M1_CooldownPerAttackSpeedRatio = 0.1f,
                M1_MaxCDFromAttackSpeed = 0.45f;

            internal static void Patch() 
            {
                if (PATCH_ACTIVE)
                    Unpatch();

                Patches.Ror2_CharacterBody.Post_RecalculateStats += Post_CharacterBody_RecalculateStats;

                PATCH_ACTIVE = true;
            }

            internal static void Unpatch() 
            {
                Patches.Ror2_CharacterBody.Post_RecalculateStats -= Post_CharacterBody_RecalculateStats;

                PATCH_ACTIVE = false;
            }

            static void Post_CharacterBody_RecalculateStats(RoR2.CharacterBody self)
            {
                if (self.name.ToLower().Contains(ArtificerBodyName.ToLower())){
                    GenericSkill m1 = self.skillLocator.primary;

                    int newBonusStock = m1.bonusStockFromBody
                        + Math.Min((int)(self.level/M1_LevelsPerCharge), M1_MaxLevelCharges)
                        + Math.Min(
                            self.inventory.GetItemCount(ItemIndex.SecondarySkillMagazine)/M1_SkillMagsPerCharge, M1_MaxSkillMagCharges
                            );
                    m1.SetBonusStockFromBody(newBonusStock);

                    m1.cooldownScale *= 1f - Mathf.Min(
                        Mathf.Max(self.attackSpeed - (self.baseAttackSpeed + self.levelAttackSpeed * self.level), 0f) * M1_CooldownPerAttackSpeedRatio,
                        1f - M1_MaxCDFromAttackSpeed
                        );
                }
            }
        }
    }
}
