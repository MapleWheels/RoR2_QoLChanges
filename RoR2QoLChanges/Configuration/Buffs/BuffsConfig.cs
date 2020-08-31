using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Configuration.Buffs
{
    [ConfigModelSectionName(Value = "Buffs")]
    public class BuffsConfig : ConfigFileModel
    {
        [ConfigEntryDefaultValue(Value = 0.5f)]
        [ConfigEntryDescription(Value ="The % increased healing from all sources per 1% hp missing.")]
        public ConfigEntry<float> MissingHpHealingBoostBuff_PercentMissingHpRatio { get; set; }
    }
}
