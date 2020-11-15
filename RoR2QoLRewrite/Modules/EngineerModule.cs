using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;
using RoR2QoLRewrite.Configuration.Survivors;
using RoR2QoLRewrite.Modules.Components;

using RoR2;
using UnityEngine;

namespace RoR2QoLRewrite.Modules
{
    class EngineerModule : IModule
    {
        private static EngineerConfig Config;
        const string EngiTurretPrefabName = "EngiTurretBody";
        const string EngiWalkerTurretPrefabName = "EngiWalkerTurretBody";

        static Components.MinionOnKillProcComponent EngiTurretComponent, EngiWalkerTurretComponent;

        public bool IsEnabled { get; private set; }
        public bool IsLoaded { get; private set; }

        public void DisableModule()
        {
            if (!IsEnabled)
                return;
            EngineerPatch.Unpatch();
            IsEnabled = false;
        }

        public void EnableModule()
        {
            if (IsEnabled || !IsLoaded)
                return;
            EngineerPatch.Patch();
            IsEnabled = true;
        }

        public void LoadModule(ConfigFile file, ManualLogSource logger)
        {
            if (IsLoaded)
                return;
            Config = file.BindModel<EngineerConfig>(logger);
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
            GameObject engiTurretPrefab = BodyCatalog.FindBodyPrefab(EngiTurretPrefabName);
            GameObject engiWalkerTurretPrefab = BodyCatalog.FindBodyPrefab(EngiWalkerTurretPrefabName);

            if (engiTurretPrefab)
                PluginCoreModule.PrefabCache.Add(EngiTurretPrefabName, engiTurretPrefab);
            if (engiWalkerTurretPrefab)
                PluginCoreModule.PrefabCache.Add(EngiWalkerTurretPrefabName, engiWalkerTurretPrefab);

            if (Config.EngiTurretOnKillProcEnabled)
            {
                EngiTurretComponent = engiTurretPrefab.AddComponent<Components.MinionOnKillProcComponent>();
                EngiWalkerTurretComponent = engiWalkerTurretPrefab.AddComponent<Components.MinionOnKillProcComponent>();

                EngiTurretComponent.OnKillProcChance = Config.EngiTurret_ChanceOnKillProcAppliedToEngi;
                EngiWalkerTurretComponent.OnKillProcChance = Config.EngiTurret_ChanceOnKillProcAppliedToEngi;
            }
        }

        private void EntityUnpatch()
        {
            if (EngiTurretComponent)
                GameObject.Destroy(EngiTurretComponent);

            if (EngiWalkerTurretComponent)
                GameObject.Destroy(EngiWalkerTurretComponent);

            PluginCoreModule.PrefabCache.Remove(EngiTurretPrefabName);
            PluginCoreModule.PrefabCache.Remove(EngiWalkerTurretPrefabName);
        }

        static class EngineerPatch
        {
            static bool LOADED = false;
            internal static void Patch() 
            {
                if (LOADED)
                    Unpatch();

                Patches.RoR2_GlobalEventManager.Post_OnCharacterDeath += OnCharacterDeath;
            }

            internal static void Unpatch() 
            {
                Patches.RoR2_GlobalEventManager.Post_OnCharacterDeath -= OnCharacterDeath;

                LOADED = false;                
            }

            private static void OnCharacterDeath(GlobalEventManager manager, DamageReport report)
            {
                CharacterBody selfBody = report.attackerBody;
                CharacterBody ownerBody = report.attackerOwnerMaster.GetBody();

                if (!selfBody || !ownerBody || ownerBody == selfBody)
                    return;

                MinionOnKillProcComponent component = selfBody.GetComponent<MinionOnKillProcComponent>();

                if (!component)
                    return;

                if (Util.CheckRoll(component.OnKillProcChance, ownerBody.master))
                {
                    report.attacker = ownerBody.gameObject;
                    report.attackerBody = ownerBody;
                    report.attackerBodyIndex = BodyCatalog.FindBodyIndex(report.attackerBody);
                    report.attackerMaster = ownerBody.master;
                    report.attackerTeamIndex = ownerBody.master.teamIndex;
                    report.damageInfo.attacker = ownerBody.gameObject;
                    report.damageInfo.inflictor = ownerBody.gameObject;

                    GlobalEventManager.instance.OnCharacterDeath(report);
                }
            }
        }
    }
}
