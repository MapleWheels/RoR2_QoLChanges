using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using RoR2;

namespace RoR2QoLChanges.Configuration.Mechanics
{
    public class BleedConfig : ConfigDataModel, IConfigBase
    {
        public const ItemIndex TriTipDaggerItemIndex = ItemIndex.BleedOnHit;
        public const BuffIndex BleedBuffIndex = BuffIndex.Bleeding;
        public const ProcType BleedProcIndex = ProcType.BleedOnHit;
        public const float BleedIntervalRate = 0.25f;

        public ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable this module",
            DefaultValue = true
        };

        public ConfigData<float> Bleed_BaseDamageRatio { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The bleed damage scale multiplier. Scales off of Base Damage",
            DefaultValue = 1.8f
        };

        public ConfigData<float> Dagger_ProcChance { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Item: Tri-Tip Dagger's Proc chance per item.",
            DefaultValue = 7f
        };

        public ConfigData<float> StandardBleed_TimeSecs { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The standard bleed damage debuff time.",
            DefaultValue = 4f
        };

        public ConfigData<float> ShatterspleenBleed_TimeSecs { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The Shatterspleen bleed damage debuff time.",
            DefaultValue = 2f
        };

        public override void SetDefaults()
        {
            SectionName = "Bleeding";
        }
    }
}
