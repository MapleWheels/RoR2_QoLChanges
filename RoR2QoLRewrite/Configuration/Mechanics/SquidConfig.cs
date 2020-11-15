using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Extensions.Configuration;

namespace RoR2QoLRewrite.Configuration.Mechanics
{
    public class SquidConfig : ConfigDataModel, IConfigBase
    {
        public ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enable/Disable the squid polyp changes",
            DefaultValue = true;
        };

        public ConfigData<float> DecayRate { get; set; } = new ConfigData<float>()
        {
            DescriptionString = "Sets the decay rate of the squid polyp's Hp relative to vanilla. Default=0, Vanilla=1",
            DefaultValue = 0
        };

        public override void SetDefaults() => SectionName = "SquidPolyp";

    }
}
