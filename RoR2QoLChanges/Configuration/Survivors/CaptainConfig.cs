﻿using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;

namespace RoR2QoLChanges.Configuration.Survivors
{
    [ConfigModelSectionName(Value = "Captain")]
    public class CaptainConfig : ConfigFileModel
    {
        [ConfigEntryDefaultValue(Value = 10f)]
        [ConfigEntryDescription(Value = "How much increase %MaxHp Captain's beacon heals for per level")]
        public ConfigEntry<float> Beacon_MaxHpHealingBase { get; set; }

        [ConfigEntryDefaultValue(Value = 0.75f)]
        [ConfigEntryDescription(Value = "How much increase %MaxHp Captain's beacon heals for per level")]
        public ConfigEntry<float> Beacon_MaxHpHealingRatioPerLevel { get; set; }

        [ConfigEntryDefaultValue(Value = 10f)]
        [ConfigEntryDescription(Value = "Captain's Beacon'd healing ward range at level 1.")]
        public ConfigEntry<float> Beacon_HealingDefaultRadius { get; set; }

        [ConfigEntryDefaultValue(Value = 1f)]
        [ConfigEntryDescription(Value = "Captain's Beacon'd healing ward increase per level.")]
        public ConfigEntry<float> Beacon_HealingRadiusIncreasePerLevel { get; set; }

        [ConfigEntryDefaultValue(Value = 0.5f)]
        [ConfigEntryDescription(Value = "The % increased healing from all sources per 1% hp missing.")]
        public ConfigEntry<float> MissingHpHealingBoostBuff_PercentMissingHpRatio { get; set; }
    }
}
