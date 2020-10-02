using RoR2;

using RoR2QoLChanges.Configuration.Survivors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2QoLChanges.Patches.Entities
{
    public class CommandoGrenadeChanges: MonoModPatchable
    {
        public CommandoConfig ActiveConfig;

        public override void ApplyPatches()
        {
            if (!ActiveConfig.Enabled.Value) return;

            On.RoR2.Projectile.ProjectileManager
                .FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float += ChangeCommandoGrenadeDamage;
        }

        private void ChangeCommandoGrenadeDamage(On.RoR2.Projectile.ProjectileManager.orig_FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float orig, RoR2.Projectile.ProjectileManager self, UnityEngine.GameObject prefab, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.GameObject owner, float damage, float force, bool crit, RoR2.DamageColorIndex damageColorIndex, UnityEngine.GameObject target, float speedOverride)
        {
            if (!CommandoGrenadePrefab) //Must be late init
                CommandoGrenadePrefab = ProjectileCatalog.GetProjectilePrefab(ProjectileCatalog.FindProjectileIndex("CommandoGrenadeProjectile"));

            if (CommandoGrenadePrefab && CommandoGrenadePrefab == prefab)
                damage *= ActiveConfig.GrenadeDamageCoefficient.Value / 7f;  //Vanilla default value is 7 (4x1.75)

            orig(self, prefab, position, rotation, owner, damage, force, crit, damageColorIndex, target, speedOverride);
        }

        protected GameObject CommandoGrenadePrefab;
        public CommandoGrenadeChanges(CommandoConfig config) =>  ActiveConfig = config;
        
    }
}
