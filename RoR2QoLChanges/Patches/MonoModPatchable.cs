using RoR2QoLChanges.Patches.Bugfix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Patches
{
    public class MonoModPatchable : IPatchable
    {
        public virtual void ApplyPatches() { }
        public virtual void RemovePatches() { }
    }
}
