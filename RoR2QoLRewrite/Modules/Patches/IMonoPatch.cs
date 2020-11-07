using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules.Patches
{
    interface IMonoPatch
    {
        void Patch();
        void Unpatch();
    }
}
