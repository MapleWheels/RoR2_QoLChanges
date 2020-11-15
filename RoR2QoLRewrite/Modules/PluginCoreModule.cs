using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using R2API;

using RoR2;

using RoR2QoLRewrite.Configuration;
using RoR2QoLRewrite.Configuration.General;
using RoR2QoLRewrite.Configuration.Items;
using RoR2QoLRewrite.Configuration.Mechanics;
using RoR2QoLRewrite.Configuration.Survivors;
using RoR2QoLRewrite.Modules;
using RoR2QoLRewrite.Modules.Buffs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2QoLRewrite.Modules
{
    internal class PluginCoreModule
    {
        //Configuration
        internal static GeneralConfig GeneralConfig { get; private set; }

        //Modules
        internal ArtificerModule ArtificerModule { get; private set; }
        internal BleedingModule BleedingModule { get; private set; }
        internal CaptainModule CaptainModule { get; private set; }
        internal CommandoModule CommandoModule { get; private set; }
        internal EngineerModule EngineerModule { get; private set; }
        internal FreshMeatModule FreshMeatModule { get; private set; }
        internal SquidPolypModule SquidPolypModule { get; private set; }
        internal WarbannerModule WarbannerModule { get; private set; }

        //Cache & Refs
        internal static Dictionary<string, GameObject> PrefabCache 
        {
            get
            {
                if (_PrefabCache == null)
                    _PrefabCache = new Dictionary<string, GameObject>();
                return _PrefabCache;
            } 
        }
        private static Dictionary<string, GameObject> _PrefabCache;

        internal static Dictionary<string, BuffEntryBase> BuffCatalog
        {
            get
            {
                if (_BuffCatalog == null)
                    _BuffCatalog = new Dictionary<string, BuffEntryBase>();
                return _BuffCatalog;
            }
        }
        private static Dictionary<string, BuffEntryBase> _BuffCatalog;

        internal static ConfigFile Config { get; private set; }
        internal static ManualLogSource Logger { get; private set; }

        internal bool Loaded { get; private set; } = false;

        internal void PreInit()
        {
            Logger.LogInfo("Pre-loader started.");
            InitResources();
            InitConfiguration();
            Logger.LogInfo("Pre-loader completed.");
        }

        internal void Init()
        {
            Logger.LogInfo("Loading modules.");
            InitModules();
        }

        internal void PostInit()
        {
            Logger.LogInfo("Plugin loading complete.");
        }

        internal void Reload()
        {
            Logger.LogInfo("Plugin is reloading.");
            Reset();
            Init();
        } 
        
        private void Reset()
        {
            ArtificerModule.UnloadModule();
            BleedingModule.UnloadModule();
            CaptainModule.UnloadModule();
            CommandoModule.UnloadModule();
            EngineerModule.UnloadModule();
            FreshMeatModule.UnloadModule();
            SquidPolypModule.UnloadModule();
            WarbannerModule.UnloadModule();
        }

        private void InitResources()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{ConVars.PluginName}.ror2qolchanges"))
            {
                var bundle = AssetBundle.LoadFromStream(stream);
                var provider = new AssetBundleResourcesProvider(ConVars.ModPrefix.TrimEnd(':'), bundle);
                ResourcesAPI.AddProvider(provider);
            }

            BuffCatalog[nameof(MissingHpHealingBoostBuff)] = new MissingHpHealingBoostBuff();
            BuffCatalog[nameof(MissingHpHealingBoostBuff)].Init();
        }

        private void InitConfiguration()
        {
            GeneralConfig = Config.BindModel<GeneralConfig>(Logger);
        }

        private void InitModules()
        {
            ArtificerModule = new ArtificerModule();
            BleedingModule = new BleedingModule();
            CaptainModule = new CaptainModule();
            CommandoModule = new CommandoModule();
            EngineerModule = new EngineerModule();
            FreshMeatModule = new FreshMeatModule();
            SquidPolypModule = new SquidPolypModule();
            WarbannerModule = new WarbannerModule();

            ArtificerModule.LoadModule(Config, Logger);
            BleedingModule.LoadModule(Config, Logger);
            CaptainModule.LoadModule(Config, Logger);
            CommandoModule.LoadModule(Config, Logger);
            EngineerModule.LoadModule(Config, Logger);
            FreshMeatModule.LoadModule(Config, Logger);
            SquidPolypModule.LoadModule(Config, Logger);
            WarbannerModule.LoadModule(Config, Logger);
        }

        public PluginCoreModule(ConfigFile configFile, ManualLogSource logger)
        {
            Config = configFile;
            Logger = logger;
        }
    }
}
