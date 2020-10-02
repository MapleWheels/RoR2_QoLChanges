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
    public class FreshMeatChanges : MonoModPatchable
    {
        public static FreshMeatConfig ActiveItemConfig { get; protected set; }

        public override void ApplyPatches()
        {
            if (!ActiveItemConfig.Enabled.Value)
                return;

            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);
            Post_RecalculateStats(self);
        }

        public static void Post_RecalculateStats(CharacterBody __instance)
        {
            if (ActiveItemConfig == null)
                return;

            float regen = __instance.regen;

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
                    __instance.regen = regen;
                }
            }
        }

        public FreshMeatChanges(FreshMeatConfig activeConfig)
        {
            ActiveItemConfig = activeConfig;
        }
    }
}
