using HarmonyLib;

using R2API.Utils;
using RoR2;

using RoR2QoLChanges.Configuration.Items;

using RoR2QoLChanges.Configuration;
using System.Reflection;

namespace RoR2QoLChanges.Patches.Items
{
    /// <summary>
    /// Applies the modified effects of Fresh Meat.
    /// </summary>
    public class HI_FreshMeatChanges : HarmonyPatchable
    {
        public static FreshMeatConfig ActiveItemConfig { get; protected set; }

        public override void ApplyPatches()
        {
            //patch RecalculateStats
            harmonyInstance.Patch(
                MI_RoR2_CharacterBody_RecalculateStats,
                prefix: new HarmonyMethod(MI_Pre_RecalculateStats),
                postfix: new HarmonyMethod(MI_Post_RecalculateStats)
                );
        }

        public static void Pre_RecalculateStats(CharacterBody __instance)
        {
            //empty for now
        }

        public static void Post_RecalculateStats(CharacterBody __instance)
        {
            if (ActiveItemConfig == null)
                return;

            //Traverse<float> regenT = Traverse.Create(__instance).Property<float>("regen");
            PropertyInfo piRegen = AccessTools.DeclaredProperty(__instance.GetType(), nameof(CharacterBody.regen));
            float regen = (float)piRegen.GetValue(__instance);

            //Fresh meat effects
            if (__instance.HasBuff(FreshMeatConfig.FreshMeatBuffIndex))
            {
                //Check if healing isn't disabled. If it has been, abort;
                if (regen > 0f)
                {
                    //Original scaling from code at Line:1489 on 2020-08-20
                    float hpRateScalar = 1f + (__instance.level - 1f) * 0.2f;
                    //Reverse the original effect first
                    regen -= hpRateScalar * 2f;

                    //Apply the new effect
                    float itemCount = __instance.inventory.GetItemCount(FreshMeatConfig.FreshMeatItemIndex);
                    float newRegenPerSec =
                        ActiveItemConfig.FreshMeat_FlatHpBase.Value
                        + ActiveItemConfig.FreshMeat_FlatHpScale.Value * __instance.level
                        + __instance.maxHealth * (
                            ActiveItemConfig.FreshMeat_MaxHpPercentBase.Value * 0.01f
                            + ActiveItemConfig.FreshMeat_MaxHpPercentScale.Value * __instance.level * 0.01f);

                    regen += newRegenPerSec;
                }
            }

            piRegen.SetValue(__instance, regen);
        }

        public HI_FreshMeatChanges(FreshMeatConfig activeConfig, Harmony instance) : base(instance)
        {
            ActiveItemConfig = activeConfig;
        }

        
        protected static MethodInfo MI_RoR2_CharacterBody_RecalculateStats { get; }
        protected static MethodInfo MI_Pre_RecalculateStats { get; }
        protected static MethodInfo MI_Post_RecalculateStats { get; }
        static HI_FreshMeatChanges()
        {
            MI_RoR2_CharacterBody_RecalculateStats = typeof(RoR2.CharacterBody).GetMethod(nameof(CharacterBody.RecalculateStats));
            MI_Pre_RecalculateStats = typeof(HI_FreshMeatChanges).GetMethod(nameof(Pre_RecalculateStats));
            MI_Post_RecalculateStats = typeof(HI_FreshMeatChanges).GetMethod(nameof(Post_RecalculateStats));
        }
    }
}
