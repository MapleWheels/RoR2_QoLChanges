using R2API;
using RoR2;

namespace RoR2QoLRewrite.Buffs
{
    public class BuffBase : CustomBuff
    {
        public BuffBase(BuffDef buffDef) : base(buffDef) { }

        public virtual BuffIndex Init() => R2API.BuffAPI.Add(this);
    }
}
