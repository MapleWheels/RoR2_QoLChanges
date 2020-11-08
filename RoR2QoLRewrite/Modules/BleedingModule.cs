using RoR2QoLRewrite.Configuration.Mechanics;
using RoR2QoLRewrite.Modules.Patches;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules
{
    class BleedingModule : IModule
    {
        private static BleedConfig Config;
        internal BleedingModule(BleedConfig config) => Config = config;

        static BuffBleedPatch BuffBleedPatch
        {
            get
            {
                if (_BuffBleedPatch == null)
                    _BuffBleedPatch = new BuffBleedPatch();
                return _BuffBleedPatch;
            }
        }
        public static BuffBleedPatch _BuffBleedPatch;

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
