using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Configuration.Survivors
{
    [ConfigModelSectionName(Value = "Captain")]
    public class CaptainConfig : ConfigFileModel
    {
        [ConfigEntryDefaultValue(Value = 0.75f)]
        [ConfigEntryDescription(Value = "How much increase %MaxHp Captain's beacon heals for per level")]
        public ConfigEntry<float> Beacon_MaxHpHealingRatioPerLevel { get; set; }

        [ConfigEntryDefaultValue(Value = 150f)]
        [ConfigEntryDescription(Value = "How much increase %MaxHp Captain's beacon heals for per level")]
        public ConfigEntry<float> Beacon_HealingRadiusIncrease { get; set; }
    }
}
