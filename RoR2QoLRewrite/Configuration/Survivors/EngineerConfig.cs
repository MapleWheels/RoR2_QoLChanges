using BepInEx.Extensions.Configuration;

namespace RoR2QoLRewrite.Configuration.Survivors
{
    public class EngineerConfig : ConfigDataModel
    {
        public static ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable this module",
            DefaultValue = true
        };

        public static ConfigData<float> EngiMissilePainer_MaxDistance { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The max targeting distance for the Engineer Painter.",
            DefaultValue = 250f
        };

        public static ConfigData<bool> EngiTurretOnKillProcEnabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable whether Engineer gets on kill proc effects from his turrets",
            DefaultValue = true
        };

        public static ConfigData<float> EngiTurret_ChanceOnKillProcAppliedToEngi { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The chance the EngiTurret OnKill effect is sent to the owner Engineer.",
            DefaultValue = 10f
        };

        public override void SetDefaults() => SectionName = "Survivors_Engineer";
    }
}
