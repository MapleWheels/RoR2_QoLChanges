using RoR2;

using RoR2_QoLChanges.Additions.Buffs;
using RoR2_QoLChanges.Additions.Mechanics;
using RoR2_QoLChanges.Configuration.Buffs;

using RoR2QoLChanges;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2_QoLChanges.Patches.Mechanics
{
    public class MMH_MissingHpHealingBoostBuff : MonoModPatchable
    {
        public static BuffsConfig ActiveConfig;

        public MMH_MissingHpHealingBoostBuff(BuffsConfig config) => ActiveConfig = config;

        public override void ApplyPatches()
        {
            On.RoR2.HealthComponent.Heal += PreHealBuffApply;
        }

        private float PreHealBuffApply(On.RoR2.HealthComponent.orig_Heal orig, RoR2.HealthComponent self, float amount, RoR2.ProcChainMask procChainMask, bool nonRegen)
        {
            CharacterBody component = self.GetComponent<CharacterBody>();
            MissingHpHealingBoostBuff buff = (MissingHpHealingBoostBuff)PluginCore.buffCatalog[nameof(MissingHpHealingBoostBuff)];

            if (component && buff != null)
                if (component.HasBuff(buff.BuffDef.buffIndex))
                    amount = GetAdjustedHealAmount(amount, self.health, self.fullHealth);

            return orig(self, amount, procChainMask, nonRegen);
        }

        public override void RemovePatches()
        {
            On.RoR2.HealthComponent.Heal -= PreHealBuffApply;
        }

        public static float GetAdjustedHealAmount(float healAmount, float currentHp, float maxHp) =>
    healAmount * (1 + ((1 - currentHp / maxHp) * ActiveConfig.MissingHpHealingBoostBuff_PercentMissingHpRatio.Value)
    );
    }
}
