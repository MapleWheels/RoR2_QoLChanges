using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;

namespace RoR2QoLChanges.Configuration.Survivors
{
    [ConfigModelSectionName(Value = "Commando")]
    public class CommandoConfig : ConfigFileModel
    {
        [ConfigEntryDescription(Value = "Commando's Grenade Alt-R damage coefficient. 1 = 100%")]
        [ConfigEntryDefaultValue(Value = 8f)]
        public ConfigEntry<float> GrenadeDamageCoefficient { get; set; }
    }
}
