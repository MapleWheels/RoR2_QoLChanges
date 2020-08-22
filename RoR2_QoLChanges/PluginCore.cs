using BepInEx;
using R2API.Utils;

using RoR2;

using RoR2_QoLChanges.Configuration;
using RoR2_QoLChanges.Patches.Bugfix;

using RoR2QoLChanges.Configuration;
using RoR2QoLChanges.Patches;
using RoR2QoLChanges.Patches.Items;

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

        void Awake()
        {
            Init();
            Logger.LogInfo($"{ConVars.PluginName} is loaded");
        }

        void Init()
        {
            activeItemsConfig = new FreshMeatConfig(Config, Logger);
            generalConfig = new GeneralConfig(Config, Logger);

            freshMeatChangesInjection = new HI_FreshMeatChanges(activeItemsConfig, HarmonyInjector.Instance);
            captainHeadCenterNull = new HI_CaptainHeadCenterNull(HarmonyInjector.Instance, generalConfig);

            freshMeatChangesInjection.ApplyPatches();
            captainHeadCenterNull.ApplyPatches();
        }
    }
}
