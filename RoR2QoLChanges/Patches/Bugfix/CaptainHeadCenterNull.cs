using HarmonyLib;

using RoR2;

using RoR2QoLChanges.Configuration;

using RoR2QoLChanges.Patches;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2QoLChanges.Patches.Bugfix
{
    public class CaptainHeadCenterNull : MonoModPatchable
    {
        protected static GeneralConfig generalConfig;

        public CaptainHeadCenterNull(GeneralConfig activeConfig)
        { 
            generalConfig = activeConfig;
        }

        public override void ApplyPatches()
        {
            On.ChildLocator.FindChild_string += ChildLocator_FindChild_string;
        }

        private Transform ChildLocator_FindChild_string(On.ChildLocator.orig_FindChild_string orig, ChildLocator self, string childName)
        {

            Transform orgResult = orig(self, childName);
            Transform result = Post_FindChild(orgResult, self, ref childName);
            return result;
        }

        public static Transform Post_FindChild(Transform __result, ChildLocator __instance, ref string childName)
        {
            if (generalConfig.CaptainOpusFix.Value)
            {
                if (childName == "HeadCenter")
                {
                    //Probably captain; this data was taken via a data dump of the transformPairs. This will probably change if Captain's model gets changed.
                    if (__instance.FindChild(2).name == "MuzzleGun" && __instance.FindChild(3).name == "MuzzleCallAirstrike1")
                    {
                        __result = __instance.FindChild(2);
                    }
                }
            }
            return __result;
        }
    }
}
