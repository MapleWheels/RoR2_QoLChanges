using RoR2;
using RoR2.Projectile;

using RoR2QoLChanges.Additions.Mechanics;
using RoR2QoLChanges.Configuration;
using RoR2QoLChanges.Configuration.Items;
using RoR2QoLChanges.Configuration.Survivors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2QoLChanges.Patches.Entities
{
    public class EntityPrefabPatches
    {
        public CommandoConfig CommandoConfig;
        public GeneralConfig GeneralConfig;

        protected void CommandoGrenadePatch()
        {
            GameObject prefab = ProjectileCatalog.GetProjectilePrefab(ProjectileCatalog.FindProjectileIndex("CommandoGrenadeProjectile"));
            if (prefab)
                prefab.AddComponent<ProjectileStickOnImpact>();
            else
                UnityEngine.Debug.LogError($"EntityPrefabPatches::CommandoGrenadePatch() | Could not load CommandoGrenadeProjectile prefab!");

        }

        protected void SquidPolypPatch()
        {
            GameObject squidBodyPrefab = BodyCatalog.FindBodyPrefab("SquidTurretBody");
            if (squidBodyPrefab)
                squidBodyPrefab.AddComponent<SquidPolypConfiguratorBehaviour>();
            else
                UnityEngine.Debug.LogError($"EntityPrefabPatches::SquidPolypPatch() | Could not load SquidTurretBody prefab!");
        }

        protected void WarbannerBuffPatch()
        {
            //GameObject prefab = Resources.Load<GameObject>("Prefabs/NetworkedObjects/WarbannerWard");       
            if (PrefabCacheHelper.Instance.TryGetPrefab("Prefabs/NetworkedObjects/WarbannerWard", out GameObject prefab))
            {
                WarbannerBuffRpcBehaviour component = prefab.AddComponent<WarbannerBuffRpcBehaviour>();
                UnityEngine.Debug.LogWarning($"Warbanner Prefab Patching: GO={prefab} | Component={component}");
            }
            else
                UnityEngine.Debug.LogError($"EntityPrefabPatches::WarbannerBuffPatch() | Could not load WarbannerWard prefab!");
        }

        protected void EngiTurretPatch()
        {
            GameObject engiTurretPrefab = BodyCatalog.FindBodyPrefab("EngiTurretBody");

            if (engiTurretPrefab)
                engiTurretPrefab.AddComponent<MinionOnKillProcBehaviour>();
            else
                UnityEngine.Debug.LogError($"EntityPrefabPatches::EngiTurretPatch() | Could not load EngiTurretBody prefab!");


            engiTurretPrefab = BodyCatalog.FindBodyPrefab("EngiWalkerTurretBody");

            if (engiTurretPrefab)
                engiTurretPrefab.AddComponent<MinionOnKillProcBehaviour>();
            else
                UnityEngine.Debug.LogError($"EntityPrefabPatches::EngiTurretPatch() | Could not load EngiWalkerTurretBody prefab!");
        }

        protected void CaptainHealBeaconPatch()
        {
            if (EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab)
                EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab.AddComponent<WardHealingBoostBehaviour>();
            else
                UnityEngine.Debug.LogError($"EntityPrefabPatches::CaptainBeaconPatch() | Could not load HealZone prefab!");
        }

        public void ApplyPatches()
        {
            CaptainHealBeaconPatch();
            if (CommandoConfig.Enabled.Value) CommandoGrenadePatch();
            EngiTurretPatch();
            if (GeneralConfig.SquidPolypEnabled.Value) SquidPolypPatch();
            WarbannerBuffPatch();
        }

        public EntityPrefabPatches(CommandoConfig commandoConfig, GeneralConfig generalConfig)
        {
            this.CommandoConfig = commandoConfig;
            this.GeneralConfig = generalConfig;
        }

        
    }
}
