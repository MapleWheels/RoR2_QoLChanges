using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Configuration
{
    [ConfigModelSectionName(Value = "General")]
    public class GeneralConfig : ConfigFileModel
    {
        [ConfigEntryDefaultValue(Value = true)]
        [ConfigEntryDescription(Value = "Enables the fix for the Captain Equipment Gorag's Opus bug")]
        public ConfigEntry<bool> CaptainOpusFix { get; set; }

        [ConfigEntryDefaultValue(Value = ConVars.Version)]
        [ConfigEntryDescription(Value = "WARNING: Do not touch if you don't know what you're doing or the config file will get nuked.")]
        public ConfigEntry<string> INTERNAL_CONFIGFILE_VERSION { get; set; }
    }
}
