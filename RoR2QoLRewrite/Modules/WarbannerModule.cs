using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoR2QoLRewrite.Configuration.Items;
using RoR2QoLRewrite.Modules.Patches;

namespace RoR2QoLRewrite.Modules
{
    class WarbannerModule : IModule
    {
        private static WarbannerConfig Config;
        internal WarbannerModule(WarbannerConfig config) => Config = config;

        static WarbannerBuffPatch WarbannerBuffPatch
        {
            get
            {
                if (_WarbannerBuffPatch == null)
                    _WarbannerBuffPatch = new WarbannerBuffPatch();
                return WarbannerBuffPatch;
            }
        }
        private static WarbannerBuffPatch _WarbannerBuffPatch;


        public bool IsEnabled => throw new NotImplementedException();

        public bool IsLoaded => throw new NotImplementedException();

        public void DisableModule()
        {
            throw new NotImplementedException();
        }

        public void EnableModule()
        {
            throw new NotImplementedException();
        }

        public void LoadModule()
        {
            throw new NotImplementedException();
        }

        public void UnloadModule()
        {
            throw new NotImplementedException();
        }
    }
}
