using RoR2;
using RoR2.Projectile;
using RoR2QoLRewrite.Configuration.Survivors;
using RoR2QoLRewrite.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoR2QoLRewrite.Modules
{
    internal class CommandoModule : ModuleBase
    {
        private const string CommandoGrenadeProjName = "CommandoGrenadeProjectile";

        protected override void OnDisable()
        {
            On.RoR2.Projectile.ProjectileManager
                .FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float -= ProjectileManager_FireProj;
            
            if (CommandoConfig.EnableStickyGrenade)
                GameObject.Destroy(Cache<ProjectileStickOnImpact>.Dispose(CommandoGrenadeProjName));

        }

        protected override void OnEnable()
        {
            On.RoR2.Projectile.ProjectileManager
                .FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float += ProjectileManager_FireProj;

            if (CommandoConfig.EnableStickyGrenade)
            {
                GameObject grenadePrefab = Cache<GameObject>.Get(CommandoGrenadeProjName);
                if (!grenadePrefab)
                {
                    grenadePrefab = ProjectileCatalog.GetProjectilePrefab(ProjectileCatalog.FindProjectileIndex(CommandoGrenadeProjName));
                    Cache<GameObject>.Add(CommandoGrenadeProjName, grenadePrefab);
                }
                Cache<ProjectileStickOnImpact>.Add(CommandoGrenadeProjName, grenadePrefab.AddComponent<ProjectileStickOnImpact>());
            }
        }

        private void ProjectileManager_FireProj(On.RoR2.Projectile.ProjectileManager.orig_FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float orig, RoR2.Projectile.ProjectileManager self, UnityEngine.GameObject prefab, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.GameObject owner, float damage, float force, bool crit, RoR2.DamageColorIndex damageColorIndex, UnityEngine.GameObject target, float speedOverride)
        {
            GameObject grenadePrefab = Cache<GameObject>.Get(CommandoGrenadeProjName);
            if (!grenadePrefab)
            {
                grenadePrefab = ProjectileCatalog.GetProjectilePrefab(ProjectileCatalog.FindProjectileIndex(CommandoGrenadeProjName));
                Cache<GameObject>.Add(CommandoGrenadeProjName, grenadePrefab);
            }

            if (prefab == grenadePrefab)
                damage *= CommandoConfig.GrenadeDamageCoefficient / 7f; //Vanilla is 7.

            orig(self, prefab, position, rotation, owner, damage, force, crit, damageColorIndex, target, speedOverride);
        }

        protected override void OnLoad()
        {
            GameObject grenadePrefab = Cache<GameObject>.Get(CommandoGrenadeProjName);
            if (!grenadePrefab)
            {
                grenadePrefab = ProjectileCatalog.GetProjectilePrefab(ProjectileCatalog.FindProjectileIndex(CommandoGrenadeProjName));
                Cache<GameObject>.Add(CommandoGrenadeProjName, grenadePrefab);
            }
        }

        protected override void OnUnload()
        {
            Cache<GameObject>.Dispose(CommandoGrenadeProjName);
        }
    }
}
