﻿using BepInEx.Configuration;
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
            InitResources();
            InitConfiguration();
        }

        internal void Init()
        {
            InitModules();
        }

        internal void PostInit()
        {

        }

        internal void Reload()
        {

        } 
        
        private void Reset()
        {
            
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
        }

        public PluginCoreModule(ConfigFile configFile, ManualLogSource logger)
        {
            Config = configFile;
            Logger = logger;
        }
    }
}