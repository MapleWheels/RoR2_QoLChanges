using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using RoR2;

namespace RoR2QoLChanges.Configuration.Items
{
    public class FreshMeatConfig : ConfigDataModel, IConfigBase
    {
        public const ItemIndex FreshMeatItemIndex = ItemIndex.RegenOnKill;
        public const BuffIndex FreshMeatBuffIndex = BuffIndex.MeatRegenBoost;

        public ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable this module",
            DefaultValue = true
        };

        public ConfigData<float> FreshMeat_MaxHpPercentBase { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Fresh Meat's %MaxHP healing at base.",
            DefaultValue = 0.5f
        };

        public ConfigData<float> FreshMeat_MaxHpPercentScale { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Fresh Meat's %MaxHP healing per extra stack.",
            DefaultValue = 0.25f
        };

        public ConfigData<float> FreshMeat_FlatHpBase { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Fresh Meat's flat healing at base.",
            DefaultValue = 2f
        };

        public ConfigData<float> FreshMeat_FlatHpScale { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Fresh Meat's flat healing per extra stack.",
            DefaultValue = 0f
        };

        public override void SetDefaults()
        {
            SectionName = "Fresh_Meat";
        }
    }
}
