using BepInEx.Extensions.Configuration;

namespace RoR2QoLRewrite.Configuration.Survivors
{
    public class CommandoConfig : ConfigDataModel, IConfigBase
    {
        public ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable this module",
            DefaultValue = true
        };

        public ConfigData<float> GrenadeDamageCoefficient { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Commando's Grenade Alt-R damage coefficient. 1 = 100% | Vanilla value is 7 (4x1.75)",
            DefaultValue = 10f
        };

        public override void SetDefaults() => SectionName = "Survivors_Commando";
    }
}
