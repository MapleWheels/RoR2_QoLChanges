using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using RoR2;

namespace RoR2_QoLChanges.Configuration
{
    [ConfigModelSectionName(Value = "Bleeding")]
    public class BleedConfig : ConfigFileModel
    {
        public const ItemIndex TriTipDaggerItemIndex = ItemIndex.BleedOnHit;
        public const BuffIndex BleedBuffIndex = BuffIndex.Bleeding;
        public const ProcType BleedProcIndex = ProcType.BleedOnHit;

        [ConfigEntryDefaultValue(Value = 1.5f)]
        [ConfigEntryDescription(Value = "The bleed damage scale multiplier. Scales off of Base Damage")]
        public ConfigEntry<float> Bleed_BaseDamageRatio { get; set; }

        [ConfigEntryDefaultValue(Value = 6f)]
        [ConfigEntryDescription(Value = "Item: Tri-Tip Dagger's Proc chance per item.")]
        public ConfigEntry<float> Dagger_ProcChance { get; set; }

        [ConfigEntryDefaultValue(Value = 4f)]
        [ConfigEntryDescription(Value = "The standard bleed damage debuff time.")]
        public ConfigEntry<float> StandardBleed_TimeSecs { get; set; }

        [ConfigEntryDefaultValue(Value = 1f)]
        [ConfigEntryDescription(Value = "The Shatterspleen bleed damage debuff time.")]
        public ConfigEntry<float> ShatterspleenBleed_TimeSecs { get; set; }
    }
}
