using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RoR2QoLRewrite.Modules.Patches;

namespace RoR2QoLRewrite.Modules
{
    class CommandoModule : IModule
    {
        static CommandoPatch CommandoPatch
        {
            get
            {
                if (_CommandoPatch == null)
                    _CommandoPatch = new CommandoPatch();
                return _CommandoPatch;
            }
        }
        private static CommandoPatch _CommandoPatch;

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
