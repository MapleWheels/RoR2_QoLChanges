using BepInEx.Configuration;
using BepInEx.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules
{
    internal abstract class ModuleBase : IModule
    {
        public bool IsEnabled { get; private set; }
        public bool Isloaded { get; private set; }

        protected ConfigFile Config;
        protected ManualLogSource Logger;

        public void Disable()
        {
            if (!IsEnabled || !Isloaded) return;
            OnDisable();
            this.IsEnabled = false;
        }

        public void Enable()
        {
            if (IsEnabled)
                return;
            if (!Isloaded)
            {
                if (this.Logger == null)
                    this.Logger = BepInEx.Logging.Logger.CreateLogSource("default");
                Logger.LogError("Attempted to enable an unloaded module.");
                return;
            }
            OnEnable();
            this.IsEnabled = true;
        }

        public void Load(ConfigFile config, ManualLogSource logger)
        {
            if (Isloaded)
                Unload();
            this.Config = config;
            this.Logger = logger;
            OnLoad();
            this.Isloaded = true;
        }

        public void Unload()
        {
            if (!Isloaded)
                return;
            if (IsEnabled)
                Disable();
            OnUnload();
            this.Config = null;
            this.Logger = null;
            this.Isloaded = false;
        }

        protected abstract void OnLoad();
        protected abstract void OnUnload();
        protected abstract void OnEnable();
        protected abstract void OnDisable();
    }
}
