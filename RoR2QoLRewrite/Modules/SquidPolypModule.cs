using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;
using RoR2;
using RoR2QoLRewrite.Configuration.Mechanics;
using RoR2QoLRewrite.Modules.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoR2QoLRewrite.Modules
{
    class SquidPolypModule : IModule
    {
        public bool IsLoaded { get; private set; }

        public bool IsEnabled { get; private set; }

        internal static SquidConfig Config;
        internal static ManualLogSource Logger;
        static SquidPolypConfiguratorComponent PolypConfiguratorComponent;
        internal const string SquidPolypBodyName = "SquidTurretBody";

        public void DisableModule()
        {
            if (!IsLoaded)
                return;

            EntityUnpatch();

            IsEnabled = false;
        }

        public void EnableModule()
        {
            if (!IsLoaded)
                return;

            EntityPatch();

            IsEnabled = true;
        }

        public void LoadModule(ConfigFile file, ManualLogSource logger)
        {
            if (IsLoaded)
                UnloadModule();

            Config = file.BindModel<SquidConfig>(logger);
            Logger = logger;
            IsLoaded = true;

            if (Config.Enabled)
                EnableModule();
        }

        public void SetConfig(ConfigFile file)
        {
            if (!IsLoaded)
                return;

            Config = file.BindModel<SquidConfig>(Logger);
        }

        public void UnloadModule()
        {
            if (!IsLoaded)
                return;

            if (IsEnabled)
                DisableModule();

            IsLoaded = false;
        }

        private void EntityPatch()
        {
            GameObject squidPolypPrefab = BodyCatalog.FindBodyPrefab(SquidPolypBodyName);
            if (!squidPolypPrefab)
            {
                Logger.LogError("SquidPolypModule::EntityPatch() | Cannot load polyp body prefab");
                return;
            }

            PolypConfiguratorComponent = squidPolypPrefab.AddComponent<SquidPolypConfiguratorComponent>();
            PolypConfiguratorComponent.DecayAdjustRatio = Config.DecayRate;
            PluginCoreModule.PrefabCache.Add(SquidPolypBodyName, squidPolypPrefab);
        }

        private void EntityUnpatch()
        {
            GameObject.Destroy(PolypConfiguratorComponent);
            PluginCoreModule.PrefabCache.Remove(SquidPolypBodyName);
        }
    }
}
