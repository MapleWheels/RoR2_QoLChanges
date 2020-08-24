using HarmonyLib;

using RoR2;

using RoR2_QoLChanges.Configuration;

using RoR2QoLChanges.Patches;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2_QoLChanges.Patches.Bugfix
{
    public class HI_CaptainHeadCenterNull : HarmonyPatchable
    {
        protected static GeneralConfig generalConfig;

        public HI_CaptainHeadCenterNull(GeneralConfig activeConfig, Harmony instance) : base (instance) 
        { 
            generalConfig = activeConfig;
        }

        public override void ApplyPatches()
        {
            harmonyInstance.Patch(
                original: MI_Global_ChildLocator_FindChild,
                postfix: new HarmonyMethod(MI_PostFindChild)
                );
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

        protected static MethodInfo MI_Global_ChildLocator_FindChild;

        protected static MethodInfo MI_PostFindChild;

        static HI_CaptainHeadCenterNull()
        {
            MI_Global_ChildLocator_FindChild = typeof(ChildLocator).GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(
                    mi => mi.GetParameters().Length == 1
                    && mi.GetParameters()[0].ParameterType == typeof(string)
                    && mi.Name == nameof(ChildLocator.FindChild)
                );

            MI_PostFindChild = typeof(HI_CaptainHeadCenterNull).GetMethod(nameof(Post_FindChild));
        }
    }
}
