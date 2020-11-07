using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RoR2QoLRewrite.Modules.Patches;

namespace RoR2QoLRewrite.Modules
{
    class CaptainModule : IModule
    {
        static CaptainPatch CaptainPatch
        {
            get
            {
                if (_CaptainPatch == null)
                    _CaptainPatch = new CaptainPatch();
                return _CaptainPatch;
            }
        }
        private static CaptainPatch _CaptainPatch;

        static BuffMissingHpHealBoostPatch BuffMissingHpHealBoostPatch
        {
            get
            {
                if (_BuffMissingHpHealBoostPatch == null)
                    _BuffMissingHpHealBoostPatch = new BuffMissingHpHealBoostPatch();
                return _BuffMissingHpHealBoostPatch;
            }
        }
        private static BuffMissingHpHealBoostPatch _BuffMissingHpHealBoostPatch;

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
