using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoR2QoLRewrite.Configuration.Items;
using RoR2QoLRewrite.Modules.Patches;

namespace RoR2QoLRewrite.Modules
{
    class FreshMeatModule : IModule
    {
        private static FreshMeatConfig Config;
        internal FreshMeatModule(FreshMeatConfig config) => Config = config;
        static FreshMeatPatch FreshMeatPatch
        {
            get
            {
                if (_FreshMeatPatch == null)
                    _FreshMeatPatch = new FreshMeatPatch();
                return _FreshMeatPatch;
            }
        }
        private static FreshMeatPatch _FreshMeatPatch;

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
