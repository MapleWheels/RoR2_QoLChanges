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
        private Dictionary<string, HarmonyPatchable> harmonyPatches;

        void Awake()
        {
            Init();
            Logger.LogInfo($"{ConVars.PluginName} is loaded");
        }

        void Init()
        {

            harmonyPatches = new Dictionary<string, HarmonyPatchable>();

            harmonyPatches.Add(
                nameof(HI_FreshMeatChanges), 
                new HI_FreshMeatChanges(Config.BindModel<FreshMeatConfig>(Logger), HarmonyInjector.Instance)
                );
            harmonyPatches.Add(
                nameof(HI_CaptainHeadCenterNull), 
                new HI_CaptainHeadCenterNull(Config.BindModel<GeneralConfig>(Logger), HarmonyInjector.Instance)
                );

            foreach(KeyValuePair<string, HarmonyPatchable> hp in harmonyPatches)
            {
                hp.Value.ApplyPatches();
            }
        }
    }
}
