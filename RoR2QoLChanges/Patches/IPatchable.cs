using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Patches
{
    public interface IPatchable
    {
        void ApplyPatches();
        void RemovePatches();
    }
}
