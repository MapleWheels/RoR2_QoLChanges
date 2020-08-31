using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;

using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2_QoLChanges.Configuration.Survivors
{
    [ConfigModelSectionName(Value = "Artificer")]
    public class ArtificerConfig : ConfigFileModel
    {
        public const string ArtificerBodyName = "Mage";

        [ConfigEntryDescription(Value = "The amount of levels for each extra charge in M1 Fire Bolt")]
        [ConfigEntryDefaultValue(Value = 10)]
        public ConfigEntry<int> M1Fire_LevelsPerCharge { get; set; }

        [ConfigEntryDescription(Value = "The max amount of charges gained by level by M1 Fire Bolt")]
        [ConfigEntryDefaultValue(Value = 2)]
        public ConfigEntry<int> M1Fire_MaxLevelCharges { get; set; }

        [ConfigEntryDescription(Value = "The amount of back magazines for each extra charge in M1 Fire Bolt")]
        [ConfigEntryDefaultValue(Value = 3)]
        public ConfigEntry<int> M1Fire_BackupMagsPerCharge { get; set; }

        [ConfigEntryDescription(Value = "The max amount of charges gained by backup magazines by M1 Fire Bolt")]
        [ConfigEntryDefaultValue(Value = 2)]
        public ConfigEntry<int> M1Fire_MaxBackupMagCharges { get; set; }

        [ConfigEntryDescription(Value = "The amount of back magazines for each extra charge in M1 Fire Bolt")]
        [ConfigEntryDefaultValue(Value = 0.1f)]
        public ConfigEntry<float> M1Fire_CooldownPerAttackSpeedRatio { get; set; }

        [ConfigEntryDescription(Value = "The max amount of charges gained by backup magazines by M1 Fire Bolt")]
        [ConfigEntryDefaultValue(Value = 0.25f)]
        public ConfigEntry<float> M1Fire_MaxCooldownFromAttackSpeed { get; set; }
    }
}
