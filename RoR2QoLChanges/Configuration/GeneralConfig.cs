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
    public class GeneralConfig : ConfigDataModel
    {
        public ConfigData<bool> CaptainOpusFix { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enables the fix for the Captain Equipment Gorag's Opus bug",
            DefaultValue = true
        };

        public ConfigData<bool> SquidPolypEnabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enables/Disables the changes to SquidPolyp (no health decay)",
            DefaultValue = true
        };

        public ConfigData<string> INTERNAL_CONFIGFILE_VERSION { get; set; } = new ConfigData<string>()
        {
            DescriptionString = "WARNING: Do not touch if you don't know what you're doing or the config file will get nuked.",
            DefaultValue = ConVars.Version
        };

        public override void SetDefaults()
        {
            SectionName = "General";
        }
    }
}
