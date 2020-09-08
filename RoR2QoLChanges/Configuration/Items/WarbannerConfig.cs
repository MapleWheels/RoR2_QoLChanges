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
        public ConfigEntry<float> attackSpeedPerStack;
    }
}
