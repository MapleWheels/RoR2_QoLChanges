using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using RoR2;

namespace RoR2_QoLChanges.Configuration
{
    [ConfigModelSectionName(Value = "Bleeding")]
    public class BleedConfig : ConfigFileModel
    {
        public static ItemIndex TriTipDaggerItemIndex = ItemIndex.Dagger;
        public static BuffIndex BleedBuffIndex = BuffIndex.Bleeding;

        [ConfigEntryDefaultValue(Value = 2.4f)]
        [ConfigEntryDescription(Value = "The bleed damage scale multiplier. Scales off of Base Damage")]
        public ConfigEntry<float> Bleed_BaseDamageRatio { get; set; }

        [ConfigEntryDefaultValue(Value = 0.06f)]
        [ConfigEntryDescription(Value = "Item: Tri-Tip Dagger's Proc chance per item.")]
        public ConfigEntry<float> Dagger_ProcChance { get; set; }

        public BleedConfig(ConfigFile file, ManualLogSource logger = null) : base(file, null, logger) { }
    }
}
