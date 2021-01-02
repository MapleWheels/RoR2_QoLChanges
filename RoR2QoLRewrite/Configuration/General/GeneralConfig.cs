using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx.Extensions.Configuration;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Configuration.General
{
    public class GeneralConfig : ConfigDataModel
    {
        public static ConfigData<bool> Enabled { get; set; } = new ConfigData<bool>()
        {
            DescriptionString = "Enables/Disables the whole mod",
            DefaultValue = true
        };

        public override void SetDefaults() => SectionName = "General";
    }
}
