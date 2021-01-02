using BepInEx.Extensions.Configuration;

using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Configuration.Items
{
    public class FreshMeatConfig : ConfigDataModel
    {
        public const ItemIndex FreshMeatItemIndex = ItemIndex.RegenOnKill;
        public const BuffIndex FreshMeatBuffIndex = BuffIndex.MeatRegenBoost;

        public static ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable this module",
            DefaultValue = true
        };

        public static ConfigData<float> FreshMeat_MaxHpPercentBase { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Fresh Meat's %MaxHP healing at base.",
            DefaultValue = 0.5f
        };

        public static ConfigData<float> FreshMeat_MaxHpPercentScale { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Fresh Meat's %MaxHP healing per extra stack.",
            DefaultValue = 0.25f
        };

        public static ConfigData<float> FreshMeat_FlatHpBase { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Fresh Meat's flat healing at base.",
            DefaultValue = 2f
        };

        public static ConfigData<float> FreshMeat_FlatHpScale { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Fresh Meat's flat healing per extra stack.",
            DefaultValue = 0f
        };

        public override void SetDefaults() => SectionName = "Item_FreshMeat";
    
    }
}
