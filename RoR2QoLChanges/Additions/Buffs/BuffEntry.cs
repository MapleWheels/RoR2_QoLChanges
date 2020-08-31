using R2API;

using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Additions.Buffs
{
    public class BuffEntry : CustomBuff
    {
        public BuffEntry(BuffDef buffDef) : base (buffDef) { }

        public virtual BuffIndex Init()
        {
            return R2API.BuffAPI.Add(this);
        }
    }
}
