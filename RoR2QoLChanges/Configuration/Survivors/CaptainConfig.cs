using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;

namespace RoR2QoLChanges.Configuration.Survivors
{
    public class CaptainConfig : ConfigDataModel, IConfigBase
    {
        public ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable this module",
            DefaultValue = true
        };

        public ConfigData<float> Beacon_MaxHpHealingBase { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "How much increase %MaxHp Captain's beacon heals for per level",
            DefaultValue = 10f
        };

        public ConfigData<float> Beacon_MaxHpHealingRatioPerLevel { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "How much increase %MaxHp Captain's beacon heals for per level",
            DefaultValue = 0.5f
        };

        public ConfigData<float> Beacon_HealingDefaultRadius { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Captain's Beacon's healing ward range at level 1.",
            DefaultValue = 10f
        };

        public ConfigData<float> Beacon_HealingRadiusIncreasePerLevel { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Captain's Beacon's healing ward increase per level.",
            DefaultValue = 1f
        };

        public ConfigData<float> MissingHpHealingBoostBuff_PercentMissingHpRatio { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The % increased healing from all sources per 1% hp missing.",
            DefaultValue = 0.5f
        };

        public ConfigData<float> Beacon_ShockDefaultRadius { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Captain's Beacon's healing ward range at level 1.",
            DefaultValue = 10f
        };

        public ConfigData<float> Beacon_ShockRadiusIncreasePerLevel { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Captain's Beacon's healing ward increase per level.",
            DefaultValue = 1f
        };

        public override void SetDefaults() => SectionName = "Captain";
    }
}
