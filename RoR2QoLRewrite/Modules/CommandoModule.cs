using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;
using RoR2QoLRewrite.Configuration.Survivors;

using RoR2;

using UnityEngine;
using RoR2.Projectile;

namespace RoR2QoLRewrite.Modules
{
    class CommandoModule : IModule
    {
        private static CommandoConfig Config;
        private static ManualLogSource Logger;
        private ProjectileStickOnImpact commandoGrenadeComponent;
        internal const string CommandoGrenadePrefabName = "CommandoGrenadeProjectile";

        public bool IsEnabled { get; private set; }
        public bool IsLoaded { get; private set; }

        public void DisableModule()
        {
            if (!IsEnabled)
                return;
            EntityUnpatch();
            CommandoPatch.Unpatch();
            IsEnabled = false;
        }

        public void EnableModule()
        {
            if (IsEnabled || !IsLoaded)
                return;
            EntityPatch();
            CommandoPatch.Patch();
            IsEnabled = true;
        }

        public void LoadModule(ConfigFile file, ManualLogSource logger)
        {
            if (IsLoaded)
                return;
            Config = file.BindModel<CommandoConfig>(logger);
            Logger = logger;
            CommandoPatch.GrenadeDamageRatio = Config.GrenadeDamageCoefficient;
            IsLoaded = true;
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

        private void EntityPatch()
        {
            //Sticky grenade
            GameObject prefab = RoR2.ProjectileCatalog.GetProjectilePrefab(ProjectileCatalog.FindProjectileIndex(CommandoGrenadePrefabName));
            if (prefab)
            {
                PluginCoreModule.PrefabCache.Add(CommandoGrenadePrefabName, prefab);
                if (Config.EnableStickyGrenade)
                    commandoGrenadeComponent = prefab.AddComponent<ProjectileStickOnImpact>();
            }
            else
                UnityEngine.Debug.LogError($"EntityPrefabPatches::CommandoGrenadePatch() | Could not load CommandoGrenadeProjectile prefab!");
        }

        private void EntityUnpatch()
        {
            if (commandoGrenadeComponent)
            {
                GameObject.Destroy(commandoGrenadeComponent);
                commandoGrenadeComponent = null;
            }
            PluginCoreModule.PrefabCache.Remove(CommandoGrenadePrefabName);
        }

        static class CommandoPatch
        {
            static bool LOADED = false;

            internal static float GrenadeDamageRatio = 11f;
            const float GrenadeDamageRatio_Vanilla = 7f;    //Default is 7f (4x1.75)

            internal static void Patch() 
            {
                if (LOADED)
                    Unpatch();

                Patches.Ror2_ProjectileManager.Pre_FireProjectile1 += ChangeCommandoGrenadeDamage;

                LOADED = true;
            }

            internal static void Unpatch() 
            {
                Patches.Ror2_ProjectileManager.Pre_FireProjectile1 -= ChangeCommandoGrenadeDamage;

                LOADED = false;
            }

            static void ChangeCommandoGrenadeDamage(RoR2.Projectile.ProjectileManager self, UnityEngine.GameObject prefab, ref UnityEngine.Vector3 position, ref UnityEngine.Quaternion rotation, UnityEngine.GameObject owner, ref float damage, ref float force, ref bool crit, ref RoR2.DamageColorIndex damageColorIndex, UnityEngine.GameObject target, ref float speedOverride)
            {
                if (!PluginCoreModule.PrefabCache[CommandoGrenadePrefabName])
                {
                    //Module no init
                    Logger.LogError("CommandoPatch::ChangeCommandoGrenadeDamage() | Prefab not init.");
                    return;
                }

                if (PluginCoreModule.PrefabCache[CommandoGrenadePrefabName] == prefab)
                    damage *= GrenadeDamageRatio / GrenadeDamageRatio_Vanilla;
            }
        }
    }
}
