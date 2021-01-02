using BepInEx.Extensions.Configuration;

namespace RoR2QoLRewrite.Configuration.Items
{
    public class WarbannerConfig : ConfigDataModel
    {
        public static ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable this module",
            DefaultValue = true
        };

        public static ConfigData<float> attackSpeedBase { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The attack speed gained at base.",
            DefaultValue = 0.3f
        };

        public static ConfigData<float> attackSpeedPerStack { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "The attack speed gained per stack of Warbanner.",
            DefaultValue = 0.05f
        };

        public override void SetDefaults() => SectionName = "Item_Warbanner";
    }
}
