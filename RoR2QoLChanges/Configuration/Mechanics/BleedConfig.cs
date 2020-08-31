using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using RoR2;

namespace RoR2QoLChanges.Configuration.Mechanics
{
    [ConfigModelSectionName(Value = "Bleeding")]
    public class BleedConfig : ConfigFileModel
    {
        public const ItemIndex TriTipDaggerItemIndex = ItemIndex.BleedOnHit;
        public const BuffIndex BleedBuffIndex = BuffIndex.Bleeding;
        public const ProcType BleedProcIndex = ProcType.BleedOnHit;
        public const float BleedIntervalRate = 0.25f;

        [ConfigEntryDefaultValue(Value = 1.8f)]
        [ConfigEntryDescription(Value = "The bleed damage scale multiplier. Scales off of Base Damage")]
        public ConfigEntry<float> Bleed_BaseDamageRatio { get; set; }

        [ConfigEntryDefaultValue(Value = 7f)]
        [ConfigEntryDescription(Value = "Item: Tri-Tip Dagger's Proc chance per item.")]
        public ConfigEntry<float> Dagger_ProcChance { get; set; }

        [ConfigEntryDefaultValue(Value = 4f)]
        [ConfigEntryDescription(Value = "The standard bleed damage debuff time.")]
        public ConfigEntry<float> StandardBleed_TimeSecs { get; set; }

        [ConfigEntryDefaultValue(Value = 2f)]
        [ConfigEntryDescription(Value = "The Shatterspleen bleed damage debuff time.")]
        public ConfigEntry<float> ShatterspleenBleed_TimeSecs { get; set; }
    }
}
