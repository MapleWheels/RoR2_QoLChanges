using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx.Extensions.Configuration;
using BepInEx.Configuration;
using BepInEx.Logging;

namespace RoR2QoLRewrite.Configuration.Items
{
    internal class SquidPolypConfig : ConfigDataModel
    {
        public static ConfigData<bool> Enabled = new ConfigData<bool>()
        {
            DescriptionString = "Whether or not this module is enabled.",
            DefaultValue = true
        };

        public static ConfigData<int> HealthDecayRate = new ConfigData<int>()
        {
            DescriptionString = "The health decay rate for the Squid Polyp. Vanilla = 30. default = 0",
            DefaultValue = 0
        };
    }
}
