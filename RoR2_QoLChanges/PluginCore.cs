using BepInEx;
using BepInEx.Extensions.Configuration;
using R2API.Utils;

using RoR2;

using RoR2_QoLChanges.Configuration;
using RoR2_QoLChanges.Patches.Bugfix;

using RoR2QoLChanges.Configuration;
using RoR2QoLChanges.Patches;
using RoR2QoLChanges.Patches.Items;
using System.Collections.Generic;

namespace RoR2QoLChanges
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync)]
    [BepInPlugin(ConVars.PackageName, ConVars.PluginName, ConVars.Version)]
    public class PluginCore : BaseUnityPlugin
    {
        private HI_FreshMeatChanges freshMeatChangesInjection;
        private HI_CaptainHeadCenterNull captainHeadCenterNull;

        private FreshMeatConfig activeItemsConfig;
        private GeneralConfig generalConfig;

        private Dictionary<string, HarmonyPatchable> harmonyPatches;

        void Awake()
        {
            Init();
            Logger.LogInfo($"{ConVars.PluginName} is loaded");
        }

        void Init()
        {

            harmonyPatches = new Dictionary<string, HarmonyPatchable>();

            activeItemsConfig = Config.BindModel<FreshMeatConfig>(Logger);
            generalConfig = Config.BindModel<GeneralConfig>(Logger);

            harmonyPatches.Add(nameof(HI_FreshMeatChanges), new HI_FreshMeatChanges(activeItemsConfig, HarmonyInjector.Instance));
            harmonyPatches.Add(nameof(HI_CaptainHeadCenterNull), new HI_CaptainHeadCenterNull(generalConfig, HarmonyInjector.Instance));


            foreach(KeyValuePair<string, HarmonyPatchable> hp in harmonyPatches)
            {
                hp.Value.ApplyPatches();
            }
        }
    }
}
