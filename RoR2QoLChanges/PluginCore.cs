using BepInEx;
using BepInEx.Extensions.Configuration;

using R2API;
using R2API.Utils;

using RoR2;

using RoR2QoLChanges.Additions.Buffs;
using RoR2QoLChanges.Additions.Mechanics;
using RoR2QoLChanges.Configuration;
using RoR2QoLChanges.Configuration.Items;
using RoR2QoLChanges.Configuration.Mechanics;
using RoR2QoLChanges.Configuration.Survivors;
using RoR2QoLChanges.Patches;
using RoR2QoLChanges.Patches.Bugfix;
using RoR2QoLChanges.Patches.Entities;
using RoR2QoLChanges.Patches.Items;
using RoR2QoLChanges.Patches.Mechanics;

using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

namespace RoR2QoLChanges
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod)]
    [BepInPlugin(ConVars.PackageName, ConVars.PluginName, ConVars.Version)]
    [R2APISubmoduleDependency(nameof(BuffAPI), nameof(ItemAPI), nameof(ResourcesAPI))]
    public class PluginCore : BaseUnityPlugin
    {
        public static Dictionary<string, HarmonyPatchable> harmonyPatches;
        public static Dictionary<string, MonoModPatchable> monoModPatches;
        public static Dictionary<string, BuffEntry> buffCatalog;
        public static GeneralConfig GeneralConfig;
        public static Sprite Assets;

        //Init
        void Awake()
        {
            Init();
            Logger.LogInfo($"{ConVars.PluginName} is loaded");
        }

        //Late Init
        void Start()
        {
            LateInit();
        }

        void Init()
        {
            //The order matters here.
            InitResources();
            InitConfig();
            InitBuffs();
            InitHooks();
        }

        void LateInit()
        {
            InitSystemInstances();
        }

        void InitResources()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{ConVars.PluginName}.ror2qolchanges"))
            {
                var bundle = AssetBundle.LoadFromStream(stream);
                var provider = new AssetBundleResourcesProvider(ConVars.ModPrefix.TrimEnd(':'), bundle);
                ResourcesAPI.AddProvider(provider);
            }
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
                nameof(Patches.Mechanics.MMH_MissingHpHealingBoostBuff),
                new MMH_MissingHpHealingBoostBuff(Config.BindModel<Configuration.Survivors.CaptainConfig>(Logger))
                );

            monoModPatches.Add(
                nameof(MMH_EngiTurretOnKillEffect),
                new MMH_EngiTurretOnKillEffect(Config.BindModel<EngineerConfig>(Logger))
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
            buffCatalog.Add(
                nameof(Additions.Buffs.MissingHpHealingBoostBuff),
                new MissingHpHealingBoostBuff(Config.BindModel<Configuration.Survivors.CaptainConfig>(Logger))
                );

            foreach (KeyValuePair<string, BuffEntry> buff in buffCatalog)
                buff.Value.Init();
        }

        void InitSystemInstances()
        {
            if (EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab)
                if(!EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab.GetComponent<MissingHpHealingBoostBehaviour>())
                    EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab.AddComponent<MissingHpHealingBoostBehaviour>();

            GameObject engiTurretPrefab = BodyCatalog.FindBodyPrefab("EngiTurretBody");
            UnityEngine.Debug.LogWarning($"PluginCore:InitSystemInstances()|bodyIndex={engiTurretPrefab}");

            if (engiTurretPrefab)
                engiTurretPrefab.AddComponent<MinionOnKillProcBehaviour>();

            engiTurretPrefab = BodyCatalog.FindBodyPrefab("EngiWalkerTurretBody");
            UnityEngine.Debug.LogWarning($"PluginCore:InitSystemInstances()|bodyIndex={engiTurretPrefab}");

            if (engiTurretPrefab)
                engiTurretPrefab.AddComponent<MinionOnKillProcBehaviour>();
        }

    }
}
