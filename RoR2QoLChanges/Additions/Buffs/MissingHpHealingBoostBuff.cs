using RoR2;
using RoR2QoLChanges.Configuration;
using RoR2QoLChanges.Configuration.Survivors;


namespace RoR2QoLChanges.Additions.Buffs
{
    public class MissingHpHealingBoostBuff : BuffEntry
    {
        private const string IconPath = ConVars.ModPrefix + ConVars.Assets.Icons.Buffs.MissingHpHealingBoostBuff;
        public static UnityEngine.Color BuffColor { get; protected set; } = new UnityEngine.Color(255, 255, 255);
        public static CaptainConfig ActiveConfig;
        public static BuffDef DefaultBuffDef { get; protected set; } = new BuffDef()
        {
            name = nameof(MissingHpHealingBoostBuff),
            buffColor = BuffColor,
            canStack = false,
            isDebuff = false,
            iconPath = IconPath
        };

        public MissingHpHealingBoostBuff(CaptainConfig buffsConfig) : base (DefaultBuffDef) => ActiveConfig = buffsConfig;
    }
}
