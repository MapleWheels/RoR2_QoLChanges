using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;

using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Configuration.Survivors
{
    [ConfigModelSectionName(Value = "Engineer")]
    public class EngineerConfig : ConfigFileModel
    {
        public const string EngiCarbonizerTurretBodyName = "EngiWalker";

        [ConfigEntryDefaultValue(Value = 250f)]
        [ConfigEntryDescription(Value = "The max targeting distance for the Engineer Painter.")]
        public ConfigEntry<float> EngiMissilePainer_MaxDistance { get; set; }

        [ConfigEntryDefaultValue(Value = 10f)]
        [ConfigEntryDescription(Value = "The chance the EngiTurret OnKill effect is sent to the owner Engineer.")]
        public ConfigEntry<float> EngiTurret_ChanceOnKillProcAppliedToEngi { get; set; }
    }
}
