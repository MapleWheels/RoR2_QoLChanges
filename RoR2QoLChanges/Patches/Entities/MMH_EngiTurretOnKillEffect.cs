using RoR2QoLChanges.Additions.Mechanics;
using RoR2QoLChanges.Configuration.Survivors;
using RoR2;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoR2QoLChanges.Patches.Entities
{
    public class MMH_EngiTurretOnKillEffect : MonoModPatchable
    {
        public static EngineerConfig ActiveConfig;

        public MMH_EngiTurretOnKillEffect(EngineerConfig config) => ActiveConfig = config;

        public override void ApplyPatches()
        {
            On.RoR2.GlobalEventManager.OnCharacterDeath += OnEngiTurretKillEffects;
            On.RoR2.CharacterBody.SendConstructTurret += OnEngiTurretSpawnCommand;
            On.RoR2.CharacterBody.HandleConstructTurret += OnEngiTurretSpawned;
            UnityEngine.Debug.LogWarning($"MMH_EngiTurret..::ApplyPatches()|Init Good.");
        }

        private void OnEngiTurretSpawnCommand(On.RoR2.CharacterBody.orig_SendConstructTurret orig, CharacterBody self, CharacterBody builder, Vector3 position, Quaternion rotation, MasterCatalog.MasterIndex masterIndex)
        {
            UnityEngine.Debug.LogWarning($"MMH_EngiTurret..::OnEngiTurretSpawnCommand()|masterIndex={masterIndex}");

            GameObject masterTurretPrefab = MasterCatalog.GetMasterPrefab(masterIndex);

            UnityEngine.Debug.LogWarning($"MMH_EngiTurret..::OnEngiTurretSpawnCommand()|masterTurretPrefab={masterTurretPrefab}");

            var component = masterTurretPrefab.GetComponent<MinionOnKillProcBehaviour>();

            if (!component)
                component = masterTurretPrefab.AddComponent<MinionOnKillProcBehaviour>();

            UnityEngine.Debug.LogWarning($"MMH_EngiTurret..::OnEngiTurretSpawnCommand()|component={component}");

            component.ChanceToPassOnHit = ActiveConfig.EngiTurret_ChanceOnKillProcAppliedToEngi.Value;

            orig(self, builder, position, rotation, masterIndex);
        }

        private void OnEngiTurretSpawned(On.RoR2.CharacterBody.orig_HandleConstructTurret orig, UnityEngine.Networking.NetworkMessage netMsg)
        {
            UnityEngine.Debug.LogWarning($"MMH_EngiTurret..::OnEngiTurretSpawned()|netMsg={netMsg}");

            GameObject masterTurretPrefab = MasterCatalog.GetMasterPrefab(netMsg.ReadMessage<CharacterBody.ConstructTurretMessage>().turretMasterIndex);

            UnityEngine.Debug.LogWarning($"MMH_EngiTurret..::OnEngiTurretSpawned()|masterTurretPrefab={masterTurretPrefab}");


            var component = masterTurretPrefab.GetComponent<MinionOnKillProcBehaviour>();

            if (!component)
                component = masterTurretPrefab.AddComponent<MinionOnKillProcBehaviour>();

            UnityEngine.Debug.LogWarning($"MMH_EngiTurret..::OnEngiTurretSpawned()|component={component}");


            component.ChanceToPassOnHit = ActiveConfig.EngiTurret_ChanceOnKillProcAppliedToEngi.Value;

            orig(netMsg);
        }

        private void OnEngiTurretKillEffects(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, RoR2.GlobalEventManager self, RoR2.DamageReport damageReport)
        {
            orig(self, damageReport);

            UnityEngine.Debug.LogWarning($"MMH_EngiTurret..::OnEngiTurretKillEffects()|damageReport={damageReport}");

            MinionOnKillProcBehaviour component = damageReport.attacker.GetComponent<MinionOnKillProcBehaviour>();

            if (component)
                component.ProcessAttackerOnKillEffects(damageReport);
        }
    }
}
