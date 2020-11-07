using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;

using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Configuration.Survivors
{
    public class ArtificerConfig : ConfigDataModel, IConfigBase
    {
        public const string ArtificerBodyName = "Mage";

        public ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable this module",
            DefaultValue = true
        };

        public ConfigData<int> M1Fire_LevelsPerCharge { get; set; } = new ConfigData<int>()
        {
            DescriptionString = "The amount of levels for each extra charge in M1 Fire Bolt",
            DefaultValue = 10
        };
        public ConfigData<int> M1Fire_MaxLevelCharges { get; set; } = new ConfigData<int>()
        {
            DescriptionString = "The max amount of charges gained by level by M1 Fire Bolt",
            DefaultValue = 99
        };
        public ConfigData<int> M1Fire_BackupMagsPerCharge { get; set; } = new ConfigData<int>()
        {
            DescriptionString = "The amount of back magazines for each extra charge in M1 Fire Bolt",
            DefaultValue = 3
        };
        public ConfigData<int> M1Fire_MaxBackupMagCharges { get; set; } = new ConfigData<int>()
        {
            DescriptionString = "The max amount of charges gained by backup magazines by M1 Fire Bolt",
            DefaultValue = 99
        };
        public ConfigData<float> M1Fire_CooldownPerAttackSpeedRatio { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The % cooldown reduction gained per 1% bonus attack speed.",
            DefaultValue = 0.1f
        };
        public ConfigData<float> M1Fire_MaxCooldownFromAttackSpeed { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The max % cooldown reduction that can be gained from attack speed.",
            DefaultValue = 0.45f
        };

        public override void SetDefaults() => SectionName = "Survivors_Artificer";
    }
}
