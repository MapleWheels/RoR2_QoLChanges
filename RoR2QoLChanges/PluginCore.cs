using BepInEx;
using BepInEx.Extensions.Configuration;

using R2API;
using R2API.Networking;
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
    [BepInDependency(MiniRpcLib.MiniRpcPlugin.Dependency)]
    [BepInDependency(BepInEx.Extensions.LibraryInfo.BepInDependencyID)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod)]
    [BepInPlugin(ConVars.PackageName, ConVars.PluginName, ConVars.Version)]
    [R2APISubmoduleDependency(nameof(BuffAPI), nameof(ItemAPI), nameof(ResourcesAPI), nameof(NetworkingAPI))]
    public class PluginCore : BaseUnityPlugin
    {
        public static Dictionary<string, IPatchable> HookPatches;
        public static Dictionary<string, BuffEntry> buffCatalog;
        public static EntityPrefabPatches EntityPatcher;
        public static GeneralConfig GeneralConfig;
        public static Sprite Assets;
        public static WarbannerBuffHelper WarbannerBuffHelper;

        //Init
        void Awake()
        {
            //OLD PLUGIN DISABLED
            //Init();
            Logger.LogInfo($"{ConVars.PluginName} is loaded");
        }

        //Late Init
        void Start()
        {
            //OLD PLUGIN DISABLED
            //LateInit();
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
            HookPatches = new Dictionary<string, IPatchable>();

            //Harmony


            //Monomod
            HookPatches.Add(
                nameof(FreshMeatChanges),
                new FreshMeatChanges(Config.BindModel<FreshMeatConfig>(Logger))
                );
            HookPatches.Add(
                nameof(CaptainHeadCenterNull),
                new CaptainHeadCenterNull(Config.BindModel<GeneralConfig>(Logger))
                );
            HookPatches.Add(
                nameof(ArtificerChanges),
                new ArtificerChanges(Config.BindModel<ArtificerConfig>(Logger))
                );
            HookPatches.Add(
                nameof(EngineerChanges),
                new EngineerChanges(Config.BindModel<EngineerConfig>(Logger))
                );
    
            HookPatches.Add(
                nameof(BleedChanges),
                new BleedChanges(Config.BindModel<BleedConfig>(Logger))
                );

            HookPatches.Add(
                nameof(Patches.Mechanics.MissingHpHealingBoostBuffPatch),
                new Patches.Mechanics.MissingHpHealingBoostBuffPatch(Config.BindModel<CaptainConfig>(Logger))
                );

            HookPatches.Add(
                nameof(EngiTurretOnKillEffect),
                new EngiTurretOnKillEffect(Config.BindModel<EngineerConfig>(Logger))
                );

            HookPatches.Add(
                nameof(WarbannerChanges),
                new WarbannerChanges(Config.BindModel<WarbannerConfig>(Logger))
                );

            HookPatches.Add(
                nameof(CommandoGrenadeChanges),
                new CommandoGrenadeChanges(Config.BindModel<CommandoConfig>(Logger))
                );

            //Patch Calls
            foreach (KeyValuePair<string, IPatchable> mp in HookPatches)
                mp.Value.ApplyPatches();
        }

        void InitBuffs()
        {
            buffCatalog = new Dictionary<string, BuffEntry>();
            buffCatalog.Add(
                nameof(Additions.Buffs.MissingHpHealingBoostBuff),
                new Additions.Buffs.MissingHpHealingBoostBuff(Config.BindModel<CaptainConfig>(Logger))
                );

            foreach (KeyValuePair<string, BuffEntry> buff in buffCatalog)
                buff.Value.Init();
        }

        void InitSystemInstances()
        {
            WarbannerBuffHelper = new WarbannerBuffHelper();

            EntityPatcher = new EntityPrefabPatches(Config.BindModel<CommandoConfig>(Logger), Config.BindModel<GeneralConfig>(Logger));
            EntityPatcher.ApplyPatches();
        }

    }
}
