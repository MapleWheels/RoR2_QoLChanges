using RoR2QoLRewrite.Configuration.Survivors;
using RoR2QoLRewrite.Modules.Patches;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules
{
    class ArtificerModule : IModule
    {
        private static ArtificerConfig Config;
        internal ArtificerModule(ArtificerConfig config) => Config = config;

        static ArtificerPatch ArtificerPatch
        {
            get
            {
                if (_artificerPatch == null)
                    _artificerPatch = new ArtificerPatch();
                return _artificerPatch;
            }
        }
        private static ArtificerPatch _artificerPatch;

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
