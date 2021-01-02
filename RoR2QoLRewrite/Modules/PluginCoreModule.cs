using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Extensions.Configuration;
using RoR2QoLRewrite.Configuration.General;
using RoR2QoLRewrite.Configuration.Items;
using RoR2QoLRewrite.Configuration.Mechanics;
using RoR2QoLRewrite.Configuration.Survivors;
using System.Reflection;
using UnityEngine;
using RoR2QoLRewrite.Configuration;
using R2API;
using RoR2QoLRewrite.Util;

namespace RoR2QoLRewrite.Modules
{
    internal class PluginCoreModule : ModuleBase
    {
        protected override void OnDisable()
        {
            DisableModules();
        }

        protected override void OnEnable()
        {
            EnableModules();
        }

        protected override void OnLoad()
        {
            InitConfig();
            InitResources();
            InitModules();
        }

        protected override void OnUnload()
        {
            Dispose();
        }

        //Init Config
        private void InitConfig()
        {
            Config.BindModel<ArtificerConfig>(Logger);
            Config.BindModel<BleedConfig>(Logger);
            Config.BindModel<CaptainConfig>(Logger);
            Config.BindModel<CommandoConfig>(Logger);
            Config.BindModel<EngineerConfig>(Logger);
            Config.BindModel<FreshMeatConfig>(Logger);
            Config.BindModel<GeneralConfig>(Logger);
            Config.BindModel<GoragsOpusConfig>(Logger);
            Config.BindModel<SquidPolypConfig>(Logger);
            Config.BindModel<WarbannerConfig>(Logger);
        }

        //init Resources
        private void InitResources()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{ConVars.PluginName}.Resources.ror2qolchanges"))
            {
                var bundle = AssetBundle.LoadFromStream(stream);
                var provider = new AssetBundleResourcesProvider(ConVars.ModPrefix.TrimEnd(':'), bundle);
                ResourcesAPI.AddProvider(provider);
            }
        }

        //Init Modules
        private void InitModules()
        {
            StaticCache.Add(new ArtificerModule());
            StaticCache.Add(new BleedingModule());
            StaticCache.Add(new CaptainModule());
            StaticCache.Add(new CommandoModule());
            StaticCache.Add(new EngineerModule());
            StaticCache.Add(new FreshMeatModule());
            StaticCache.Add(new GoragsOpusModule());
            StaticCache.Add(new SquidPolypModule());
            StaticCache.Add(new WarbannerModule());

            StaticCache.Get<ArtificerModule>().Load(Config, Logger);
            StaticCache.Get<BleedingModule>().Load(Config, Logger);
            StaticCache.Get<CaptainModule>().Load(Config, Logger);
            StaticCache.Get<CommandoModule>().Load(Config, Logger);
            StaticCache.Get<EngineerModule>().Load(Config, Logger);
            StaticCache.Get<FreshMeatModule>().Load(Config, Logger);
            StaticCache.Get<GoragsOpusModule>().Load(Config, Logger);
            StaticCache.Get<SquidPolypModule>().Load(Config, Logger);
            StaticCache.Get<WarbannerModule>().Load(Config, Logger);
        }

        //Enable Modules
        private void EnableModules()
        {
            if (ArtificerConfig.Enabled)
                StaticCache.Get<ArtificerModule>().Enable();
            if (BleedConfig.Enabled)
                StaticCache.Get<BleedingModule>().Enable();
            if (CaptainConfig.Enabled)
                StaticCache.Get<CaptainModule>().Enable();
            if (CommandoConfig.Enabled)
                StaticCache.Get<CommandoModule>().Enable();
            if (EngineerConfig.Enabled)
                StaticCache.Get<EngineerModule>().Enable();
            if (FreshMeatConfig.Enabled)
                StaticCache.Get<FreshMeatModule>().Enable();
            if (GoragsOpusConfig.Enabled)
                StaticCache.Get<GoragsOpusModule>().Enable();
            if (SquidPolypConfig.Enabled)
                StaticCache.Get<SquidPolypModule>().Enable();
            if (WarbannerConfig.Enabled)
                StaticCache.Get<WarbannerModule>().Enable();
        }

        //Disable Modules
        private void DisableModules()
        {
            StaticCache.Get<ArtificerModule>().Disable();
            StaticCache.Get<BleedingModule>().Disable();
            StaticCache.Get<CaptainModule>().Disable();
            StaticCache.Get<CommandoModule>().Disable();
            StaticCache.Get<EngineerModule>().Disable();
            StaticCache.Get<FreshMeatModule>().Disable();
            StaticCache.Get<GoragsOpusModule>().Disable();
            StaticCache.Get<SquidPolypModule>().Disable();
            StaticCache.Get<WarbannerModule>().Disable();
        }

        //Dispose of all resources
        private void Dispose()
        {
            StaticCache.Get<ArtificerModule>().Unload();
            StaticCache.Get<BleedingModule>().Unload();
            StaticCache.Get<CaptainModule>().Unload();
            StaticCache.Get<CommandoModule>().Unload();
            StaticCache.Get<EngineerModule>().Unload();
            StaticCache.Get<FreshMeatModule>().Unload();
            StaticCache.Get<GoragsOpusModule>().Unload();
            StaticCache.Get<SquidPolypModule>().Unload();
            StaticCache.Get<WarbannerModule>().Unload();

            StaticCache.Dispose<ArtificerModule>();
            StaticCache.Dispose<BleedingModule>();
            StaticCache.Dispose<CaptainModule>();
            StaticCache.Dispose<CommandoModule>();
            StaticCache.Dispose<EngineerModule>();
            StaticCache.Dispose<FreshMeatModule>();
            StaticCache.Dispose<GoragsOpusModule>();
            StaticCache.Dispose<SquidPolypModule>();
            StaticCache.Dispose<WarbannerModule>();
        }
    }
}
