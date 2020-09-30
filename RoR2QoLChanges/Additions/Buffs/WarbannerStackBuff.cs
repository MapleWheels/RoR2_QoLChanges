using RoR2;

using RoR2QoLChanges.Configuration;
using RoR2QoLChanges.Configuration.Items;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Additions.Buffs
{
    public class WarbannerStackBuff : BuffEntry
    {
        private const string IconPath = ConVars.ModPrefix + ConVars.Assets.Icons.Buffs.MissingHpHealingBoostBuff;   //Temp, to be replaced with a new buff icon.
        public static UnityEngine.Color BuffColor { get; protected set; } = new UnityEngine.Color(255, 255, 255);
        public static WarbannerConfig ActiveConfig;
        public static BuffDef DefaultBuffDef { get; protected set; } = new BuffDef()
        {
            name = nameof(WarbannerStackBuff),
            buffColor = BuffColor,
            canStack = true,
            isDebuff = false,
            iconPath = IconPath
        };

        public WarbannerStackBuff(WarbannerConfig buffsConfig) : base(DefaultBuffDef) => ActiveConfig = buffsConfig;
    }
}
