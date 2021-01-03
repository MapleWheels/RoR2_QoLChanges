using RoR2;

using RoR2QoLRewrite.Configuration;

namespace RoR2QoLRewrite.Buffs
{
    public class HyperWarCryBuff : BuffBase
    {
        //TODO: change HUD icon asset.
        private const string IconPath = ConVars.ModPrefix + ConVars.Assets.Icons.Buffs.MissingHpHealingBoostBuff;
        public static UnityEngine.Color BuffColor { get; protected set; } = new UnityEngine.Color(255, 255, 255);
        public static BuffDef DefaultBuffDef { get; protected set; } = new BuffDef()
        {
            name = nameof(HyperWarCryBuff),
            buffColor = BuffColor,
            canStack = false,
            isDebuff = false,
            iconPath = IconPath
        };

        public HyperWarCryBuff() : base(DefaultBuffDef) { }
    }
}
