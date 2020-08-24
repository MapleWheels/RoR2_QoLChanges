using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using RoR2;

namespace RoR2QoLChanges.Configuration
{
    [ConfigModelSectionName(Value = "Fresh_Meat")]
    public class FreshMeatConfig : ConfigFileModel
    {
        public const ItemIndex FreshMeatItemIndex = ItemIndex.RegenOnKill;
        public const BuffIndex FreshMeatBuffIndex = BuffIndex.MeatRegenBoost;

        [ConfigEntryDefaultValue(Value = 0.5f)]
        [ConfigEntryDescription(Value = "Fresh Meat's %MaxHP healing at base.")]
        public ConfigEntry<float> FreshMeat_MaxHpPercentBase { get; set; }


        [ConfigEntryDefaultValue(Value = 0.25f)]
        [ConfigEntryDescription(Value = "Fresh Meat's %MaxHP healing per extra stack.")]
        public ConfigEntry<float> FreshMeat_MaxHpPercentScale { get; set; }


        [ConfigEntryDefaultValue(Value = 2f)]
        [ConfigEntryDescription(Value = "Fresh Meat's flat healing at base.")]
        public ConfigEntry<float> FreshMeat_FlatHpBase { get; set; }

        [ConfigEntryDefaultValue(Value = 0f)]
        [ConfigEntryDescription(Value = "Fresh Meat's flat healing per extra stack.")]
        public ConfigEntry<float> FreshMeat_FlatHpScale { get; set; }
    }
}
