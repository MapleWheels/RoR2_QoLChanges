using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RoR2;

using RoR2QoLRewrite.Configuration;
using RoR2QoLRewrite.Configuration.Survivors;

namespace RoR2QoLRewrite.Modules.Buffs
{
    public class MissingHpHealingBoostBuff : BuffEntryBase
    {
        private const string IconPath = ConVars.ModPrefix + ConVars.Assets.Icons.Buffs.MissingHpHealingBoostBuff;
        public static UnityEngine.Color BuffColor { get; protected set; } = new UnityEngine.Color(255, 255, 255);
        public static BuffDef DefaultBuffDef { get; protected set; } = new BuffDef()
        {
            name = nameof(MissingHpHealingBoostBuff),
            buffColor = BuffColor,
            canStack = false,
            isDebuff = false,
            iconPath = IconPath
        };

        public MissingHpHealingBoostBuff() : base(DefaultBuffDef) { }
    }
}
