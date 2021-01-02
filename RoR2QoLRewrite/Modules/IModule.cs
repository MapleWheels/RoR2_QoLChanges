using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx.Configuration;
using BepInEx.Logging;

namespace RoR2QoLRewrite.Modules
{
    interface IModule
    {
        bool IsEnabled { get; }
        bool Isloaded { get; }
        void Load(ConfigFile config, ManualLogSource logger);
        void Unload();
        void Enable();
        void Disable();
    }
}
