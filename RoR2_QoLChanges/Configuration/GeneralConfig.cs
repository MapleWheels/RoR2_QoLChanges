using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2_QoLChanges.Configuration
{
    [ConfigModelSectionName(Value = "General")]
    public class GeneralConfig : ConfigFileModel
    {
        [ConfigEntryDefaultValue(Value = true)]
        [ConfigEntryDescription(Value = "Enables the fix for the Captain Equipment Gorag's Opus bug")]
        public ConfigEntry<bool> CaptainOpusFix { get; set; }

        public GeneralConfig(ConfigFile file, ManualLogSource logger) : base(file, null, logger) { }
    }
}
