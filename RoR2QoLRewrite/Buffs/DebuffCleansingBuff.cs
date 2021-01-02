using RoR2;

using RoR2QoLRewrite.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Buffs
{
    public class DebuffCleansingBuff : BuffBase
    {
        private const string IconPath = ConVars.ModPrefix + ConVars.Assets.Icons.Buffs.MissingHpHealingBoostBuff;
        public static UnityEngine.Color BuffColor { get; protected set; } = new UnityEngine.Color(255, 255, 255);
        public static BuffDef DefaultBuffDef { get; protected set; } = new BuffDef()
        {
            name = nameof(DebuffCleansingBuff),
            buffColor = BuffColor,
            canStack = false,
            isDebuff = false,
            iconPath = IconPath
        };

        public DebuffCleansingBuff() : base(DefaultBuffDef) { }
    }
}
