using RoR2;
using R2API;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules.Buffs
{
    public class BuffEntryBase : CustomBuff
    {
        public BuffEntryBase(BuffDef buffDef) : base(buffDef) { }
        public virtual BuffIndex Init() => R2API.BuffAPI.Add(this);
    }
}
