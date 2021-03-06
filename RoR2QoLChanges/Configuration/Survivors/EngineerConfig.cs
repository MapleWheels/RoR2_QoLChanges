﻿using BepInEx.Extensions.Configuration;

namespace RoR2QoLChanges.Configuration.Survivors
{
    public class EngineerConfig : ConfigDataModel, IConfigBase
    {
        public const string EngiCarbonizerTurretBodyName = "EngiWalker";

        public ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable this module",
            DefaultValue = true
        };

        public ConfigData<float> EngiMissilePainer_MaxDistance { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The max targeting distance for the Engineer Painter.",
            DefaultValue = 250f
        };

        public ConfigData<float> EngiTurret_ChanceOnKillProcAppliedToEngi { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The chance the EngiTurret OnKill effect is sent to the owner Engineer.",
            DefaultValue = 10f
        };

        public override void SetDefaults()
        {
            SectionName = "Engineer";
        }
    }
}
