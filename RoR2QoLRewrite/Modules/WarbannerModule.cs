using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;
using RoR2QoLRewrite.Configuration.Items;
using RoR2QoLRewrite.Modules.Components;
using RoR2QoLRewrite.Modules.Networking;
using UnityEngine;

namespace RoR2QoLRewrite.Modules
{
    class WarbannerModule : IModule
    {
        private static WarbannerConfig Config;

        public bool IsEnabled { get; private set; }
        public bool IsLoaded { get; private set; }

        internal WarbannerHelperComponent WarbannerPrefabComponent;
        internal const string WarbannerPrefabPath = "Prefabs/NetworkedObjects/WarbannerWard";
        static ManualLogSource Logger;


        public void DisableModule()
        {
            if (!IsEnabled)
                return;
            WarbannerPatch.Unpatch();
            IsEnabled = false;
        }

        public void EnableModule()
        {
            if (IsEnabled || !IsLoaded)
                return;
            WarbannerPatch.Patch();
            IsEnabled = true;
        }

        public void LoadModule(ConfigFile file, ManualLogSource logger)
        {
            if (IsLoaded)
                return;
            Config = file.BindModel<WarbannerConfig>(logger);
            WarbannerModule.Logger = logger;

            IsLoaded = true;

            if (Config.Enabled)
                EnableModule();
        }

        public void SetConfig(ConfigFile file)
        {
            if (!IsLoaded)
                return;
            Config.SetConfigFile(file);
        }

        public void UnloadModule()
        {
            if (!IsLoaded)
                return;
            if (IsEnabled)
                DisableModule();
            IsLoaded = false;
        }

        void EntityPatch()
        {
            GameObject warbannerPrefab = Resources.Load<GameObject>(WarbannerPrefabPath);
            if (!warbannerPrefab)
            {
                WarbannerModule.Logger.LogError("Warbannermodule::EntityPatch() | Could not load the warbanner ward prefab.");
                return;
            }

            R2API.Networking.NetworkingAPI.RegisterMessageType<WarbannerSyncMsg>();
            this.WarbannerPrefabComponent = warbannerPrefab.AddComponent<WarbannerHelperComponent>();
            PluginCoreModule.PrefabCache.Add(WarbannerPrefabPath, warbannerPrefab);
        }

        void EntityUnpatch()
        {
            GameObject.Destroy(WarbannerPrefabComponent);
            PluginCoreModule.PrefabCache.Remove(WarbannerPrefabPath);
        }

        static class WarbannerPatch
        {
            internal static void Patch() { }
            internal static void Unpatch() { }
        }
        

    }
}
