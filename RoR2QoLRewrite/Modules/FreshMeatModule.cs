using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;
using RoR2QoLRewrite.Configuration.Items;
using RoR2QoLRewrite.Configuration.Mechanics;

namespace RoR2QoLRewrite.Modules
{
    class FreshMeatModule : IModule
    {
        private static FreshMeatConfig Config;

        public bool IsEnabled { get; private set; }
        public bool IsLoaded { get; private set; }

        public void DisableModule()
        {
            if (!IsEnabled)
                return;
            FreshMeatPatch.Unpatch();
            IsEnabled = false;
        }

        public void EnableModule()
        {
            if (IsEnabled || !IsLoaded)
                return;
            FreshMeatPatch.Patch();
            IsEnabled = true;
        }

        public void LoadModule(ConfigFile file, ManualLogSource logger)
        {
            if (IsLoaded)
                return;

            Config = file.BindModel<FreshMeatConfig>(logger);
            FreshMeatPatch.FlatHpBase = Config.FreshMeat_FlatHpBase;
            FreshMeatPatch.FlatHpScale = Config.FreshMeat_FlatHpScale;
            FreshMeatPatch.MaxHpRatioBase = Config.FreshMeat_MaxHpPercentBase * 0.01f;
            FreshMeatPatch.MaxHpRatioPerStack = Config.FreshMeat_MaxHpPercentScale * 0.01f;

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
            IsLoaded = false;
        }

        static class FreshMeatPatch
        {
            static bool LOADED = false;
            
            internal const RoR2.BuffIndex freshMeatBuffIndex = RoR2.BuffIndex.MeatRegenBoost;
            internal const RoR2.ItemIndex freshMeatItemIndex = RoR2.ItemIndex.RegenOnKill;

            internal static float FlatHpBase = 2f;
            internal static float FlatHpScale = 0.25f;
            internal static float MaxHpRatioBase = 0.01f;
            internal static float MaxHpRatioPerStack = 0.005f;

            internal static void Patch() 
            {
                if (LOADED)
                    Unpatch();

                Patches.Ror2_CharacterBody.Post_RecalculateStats += FreshMeatRegenBoost;

                LOADED = true;
            }
            
            internal static void Unpatch() 
            {
                Patches.Ror2_CharacterBody.Post_RecalculateStats -= FreshMeatRegenBoost;

                LOADED = false;
            }

            static void FreshMeatRegenBoost(RoR2.CharacterBody self)
            {
                float regen = self.regen;

                if (regen > 0f && self.HasBuff(freshMeatBuffIndex))
                {
                    //Original fresh meat scaling from code at Line:1489 on 2020-08-20
                    regen -= 1f + (self.level - 1f) * 0.2f * 2f;    //Verbose for clarity

                    float itemCount = self.inventory.GetItemCount(freshMeatItemIndex);
                    regen += FlatHpBase + FlatHpScale * self.level + self.maxHealth * (MaxHpRatioBase + MaxHpRatioPerStack * itemCount);

                    self.regen = regen;
                }
            }
        }
    }
}
