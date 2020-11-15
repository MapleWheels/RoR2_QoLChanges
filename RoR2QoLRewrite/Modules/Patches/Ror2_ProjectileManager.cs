using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules.Patches
{
    static class Ror2_ProjectileManager
    {
        static bool _LOADED = false;

        internal delegate void ActionDelegate_FireProjectile0(RoR2.Projectile.ProjectileManager self, UnityEngine.GameObject prefab, ref UnityEngine.Vector3 position, ref UnityEngine.Quaternion rotation, UnityEngine.GameObject owner, ref float damage, ref float force, ref bool crit, ref RoR2.DamageColorIndex damageColorIndex, UnityEngine.GameObject target, ref float speedOverride);

        public static event ActionDelegate_FireProjectile0 Pre_FireProjectile1, Post_FireProjectile1;

        public static void Load()
        {
            if (_LOADED)
                Unload();

            On.RoR2.Projectile.ProjectileManager
                    .FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float += FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float;
            _LOADED = true;

        }

        public static void Unload()
        {
            On.RoR2.Projectile.ProjectileManager
                    .FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float -= FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float;

            _LOADED = false;
        }

        static void FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float(On.RoR2.Projectile.ProjectileManager.orig_FireProjectile_GameObject_Vector3_Quaternion_GameObject_float_float_bool_DamageColorIndex_GameObject_float orig, RoR2.Projectile.ProjectileManager self, UnityEngine.GameObject prefab, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.GameObject owner, float damage, float force, bool crit, RoR2.DamageColorIndex damageColorIndex, UnityEngine.GameObject target, float speedOverride)
        {
            Pre_FireProjectile1.Invoke(self, prefab, ref position, ref rotation, owner, ref damage, ref force, ref crit, ref damageColorIndex, target, ref speedOverride);
            orig(self, prefab, position, rotation, owner, damage, force, crit, damageColorIndex, target, speedOverride);
            Post_FireProjectile1.Invoke(self, prefab, ref position, ref rotation, owner, ref damage, ref force, ref crit, ref damageColorIndex, target, ref speedOverride);
        }
    }
}
