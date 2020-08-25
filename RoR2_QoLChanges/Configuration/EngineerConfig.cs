using BepInEx.Extensions.Configuration;

using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2_QoLChanges.Configuration
{
    [ConfigModelSectionName(Value = "Engineer")]
    public class EngineerConfig : ConfigFileModel
    {
        public const string EngiBodyName = "EngiBody";
        public const string EngiCarbonizerTurretBodyName = "EngiWalker";
    }
}
