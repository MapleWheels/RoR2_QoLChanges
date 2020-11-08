using BepInEx.Configuration;
using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules
{
    internal interface IModule
    {
        bool IsLoaded { get; }
        bool IsEnabled { get; }
        void SetConfig(ConfigFile file);
        void LoadModule(ConfigFile file, ManualLogSource logger);
        void UnloadModule();
        void EnableModule();
        void DisableModule();
    }
}
