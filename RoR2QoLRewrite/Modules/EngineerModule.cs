using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoR2QoLRewrite.Configuration.Survivors;
using RoR2QoLRewrite.Modules.Patches;

namespace RoR2QoLRewrite.Modules
{
    class EngineerModule : IModule
    {
        private static EngineerConfig Config;
        internal EngineerModule(EngineerConfig config) => Config = config;

        static EngineerPatch EngineerPatch
        {
            get
            {
                if (_EngineerPatch == null)
                    _EngineerPatch = new EngineerPatch();
                return _EngineerPatch;
            }
        }
        private static EngineerPatch _EngineerPatch;

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
