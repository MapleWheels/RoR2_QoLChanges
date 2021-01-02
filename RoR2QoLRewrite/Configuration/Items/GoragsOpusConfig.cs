using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx.Extensions.Configuration;

namespace RoR2QoLRewrite.Configuration.Items
{
    internal class GoragsOpusConfig : ConfigDataModel
    {
        public static ConfigData<bool> Enabled = new ConfigData<bool>()
        {
            DefaultValue = true,
            DescriptionString = "Whether or not the module is enabled."
        };

        public static ConfigData<float> GoragsOpusCooldown = new ConfigData<float>()
        {
            DefaultValue = 45f,
            DescriptionString = "Gorag's Opus cooldown time in seconds. Vanilla=45"
        };

        public static ConfigData<float> GoragsOpusDuration = new ConfigData<float>()
        {
            DefaultValue = 8f,
            DescriptionString = "Gorag's Opus duration in seconds. Vanilla=8"
        };
    }
}
