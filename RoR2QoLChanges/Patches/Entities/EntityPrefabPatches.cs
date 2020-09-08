using RoR2;
using RoR2.Projectile;

using RoR2QoLChanges.Additions.Mechanics;
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
        public CommandoConfig commandoConfig;

        protected void CommandoGrenadePatch()
        {
            GameObject prefab = ProjectileCatalog.GetProjectilePrefab(ProjectileCatalog.FindProjectileIndex("CommandoGrenadeProjectile"));
            if (prefab)
                prefab.AddComponent<ProjectileStickOnImpact>();

            EntityStates.Commando.CommandoWeapon.FireGrenade.damageCoefficient = commandoConfig.GrenadeDamageCoefficient.Value;
        }

        protected void SquidPolypPatch()
        {
            GameObject squidBodyPrefab = BodyCatalog.FindBodyPrefab("SquidTurretBody");
            if (squidBodyPrefab)
            {
                squidBodyPrefab.AddComponent<SquidPolypConfiguratorBehaviour>();
            }
        }

        protected void EngiTurretPatch()
        {
            GameObject engiTurretPrefab = BodyCatalog.FindBodyPrefab("EngiTurretBody");

            if (engiTurretPrefab)
                engiTurretPrefab.AddComponent<MinionOnKillProcBehaviour>();

            engiTurretPrefab = BodyCatalog.FindBodyPrefab("EngiWalkerTurretBody");

            if (engiTurretPrefab)
                engiTurretPrefab.AddComponent<MinionOnKillProcBehaviour>();
        }

        protected void CaptainBeaconPatch()
        {
            if (EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab)
                EntityStates.CaptainSupplyDrop.HealZoneMainState.healZonePrefab.AddComponent<MissingHpHealingBoostBehaviour>();
        }

        public void ApplyPatches()
        {
            CaptainBeaconPatch();
            CommandoGrenadePatch();
            EngiTurretPatch();
            SquidPolypPatch();
        }

        public EntityPrefabPatches(CommandoConfig commandoConfig)
        {
            this.commandoConfig = commandoConfig;
        }
    }
}
