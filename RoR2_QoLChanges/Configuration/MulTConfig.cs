using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2_QoLChanges.Configuration
{
    public class MulTConfig : ConfigFileModel
    { 


        [ConfigEntryDefaultValue(Value = 25f)]
        [ConfigEntryDescription(Value = "Buff to Mul-T's base armor stats.")]
        public ConfigEntry<float> BaseArmorBonus { get; set; }

        [ConfigEntryDefaultValue(Value = 100f)]
        [ConfigEntryDescription(Value = "Armor boost while using Utility Sprint-Dash Skill")]
        public ConfigEntry<float> DashArmorBonus { get; set; }

        public MulTConfig(ConfigFile file, ManualLogSource logger = null) : base(file, null, logger) { }
    }
}
