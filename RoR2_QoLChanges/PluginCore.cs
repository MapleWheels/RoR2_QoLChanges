using BepInEx;
using BepInEx.Extensions.Configuration;

using R2API;
using R2API.Utils;

using RoR2;

using RoR2_QoLChanges.Additions.Buffs;
using RoR2_QoLChanges.Configuration;
using RoR2_QoLChanges.Configuration.Survivors;
using RoR2_QoLChanges.Configuration.Mechanics;
using RoR2_QoLChanges.Configuration.Items;
using RoR2_QoLChanges.Patches;
using RoR2_QoLChanges.Patches.Bugfix;
using RoR2_QoLChanges.Patches.Entities;
using RoR2_QoLChanges.Patches.Mechanics;
using RoR2QoLChanges.Configuration;
using RoR2QoLChanges.Patches;
using RoR2QoLChanges.Patches.Items;
using System.Collections.Generic;
using RoR2_QoLChanges.Configuration.Buffs;

namespace RoR2QoLChanges
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod)]
    [BepInPlugin(ConVars.PackageName, ConVars.PluginName, ConVars.Version)]
    [R2APISubmoduleDependency(nameof(BuffAPI), nameof(ItemAPI))]
    public class PluginCore : BaseUnityPlugin
    {
        public static Dictionary<string, HarmonyPatchable> harmonyPatches;
        public static Dictionary<string, MonoModPatchable> monoModPatches;
        public static Dictionary<string, BuffEntry> buffCatalog;
        public static GeneralConfig GeneralConfig;

        void Awake()
        {
            Init();
            Logger.LogInfo($"{ConVars.PluginName} is loaded");
        }

        void Init()
        {
            //The order matters here.
            InitConfig();
            InitBuffs();
            InitHooks();
        }

        void InitConfig()
        {
            GeneralConfig = Config.BindModel<GeneralConfig>(Logger);
            
            if (GeneralConfig.INTERNAL_CONFIGFILE_VERSION.Value != ConVars.Version)
            {
                //Nuke time baby
                System.IO.File.Delete(Config.ConfigFilePath);
                GeneralConfig = Config.BindModel<GeneralConfig>(Logger);
            }
        }

        void InitHooks()
        {
            harmonyPatches = new Dictionary<string, HarmonyPatchable>();
            monoModPatches = new Dictionary<string, MonoModPatchable>();

            //Harmony
            harmonyPatches.Add(
                nameof(HI_FreshMeatChanges),
                new HI_FreshMeatChanges(Config.BindModel<FreshMeatConfig>(Logger), HarmonyInjector.Instance)
                );
            harmonyPatches.Add(
                nameof(HI_CaptainHeadCenterNull),
                new HI_CaptainHeadCenterNull(Config.BindModel<GeneralConfig>(Logger), HarmonyInjector.Instance)
                );
            harmonyPatches.Add(
                nameof(HI_ArtificerChanges),
                new HI_ArtificerChanges(Config.BindModel<ArtificerConfig>(Logger), HarmonyInjector.Instance)
                );
            harmonyPatches.Add(
                nameof(HI_EngineerChanges),
                new HI_EngineerChanges(Config.BindModel<EngineerConfig>(Logger), HarmonyInjector.Instance)
                );

            //Monomod
            monoModPatches.Add(
                nameof(MMH_BleedChanges),
                new MMH_BleedChanges(Config.BindModel<BleedConfig>(Logger))
                );

            monoModPatches.Add(
                nameof(MMH_MissingHpHealingBoostBuff),
                new MMH_MissingHpHealingBoostBuff(Config.BindModel<BuffsConfig>(Logger))
                );

            //Patch Calls
            foreach (KeyValuePair<string, HarmonyPatchable> hp in harmonyPatches)
                hp.Value.ApplyPatches();

            foreach (KeyValuePair<string, MonoModPatchable> mp in monoModPatches)
                mp.Value.ApplyPatches();
        }

        void InitBuffs()
        {
            buffCatalog = new Dictionary<string, BuffEntry>();

            foreach (KeyValuePair<string, BuffEntry> buff in buffCatalog)
                buff.Value.Init();
        }

    }
}
