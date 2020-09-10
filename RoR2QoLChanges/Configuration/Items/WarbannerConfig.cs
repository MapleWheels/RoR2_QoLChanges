using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Configuration.Items
{
    [ConfigModelSectionName(Value = "Item_Warbanner")]
    public class WarbannerConfig : ConfigFileModel
    {
        [ConfigEntryDefaultValue(Value = 0.3f)]
        [ConfigEntryDescription(Value = "The attack speed gained at base.")]
        public ConfigEntry<float> attackSpeedBase { get; set; }

        [ConfigEntryDefaultValue(Value = 0.05f)]
        [ConfigEntryDescription(Value = "The attack speed gained per stack of Warbanner.")]
        public ConfigEntry<float> attackSpeedPerStack { get; set; }
    }
}
