using MonoMod.Cil;

using RoR2QoLRewrite.Configuration.Mechanics;
using BepInEx.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules
{
    internal class BleedingModule : ModuleBase
    {
        protected override void OnDisable()
        {
            IL.RoR2.GlobalEventManager.OnHitEnemy -= ILPatch_GlobalEventManager_OnHitEnemy_TriTipChance;
            IL.RoR2.GlobalEventManager.OnHitEnemy -= ILPatch_GlobalEventManager_OnHitEnemy_BleedTime;

            RoR2.DotController.dotDefs[0].interval = 0.25f;
            RoR2.DotController.dotDefs[0].damageCoefficient = 0.2f;    //Vanilla value is 2.4x_baseDamage * 0.25s_perTick / 3s_totalTime
        }

        protected override void OnEnable()
        {
            IL.RoR2.GlobalEventManager.OnHitEnemy += ILPatch_GlobalEventManager_OnHitEnemy_TriTipChance;
            IL.RoR2.GlobalEventManager.OnHitEnemy += ILPatch_GlobalEventManager_OnHitEnemy_BleedTime;

            RoR2.DotController.dotDefs[0].interval = BleedConfig.BleedIntervalRate;
            RoR2.DotController.dotDefs[0].damageCoefficient = BleedConfig.Bleed_BaseDamageRatio.Value * BleedConfig.BleedIntervalRate / BleedConfig.StandardBleed_TimeSecs.Value;
        }

        protected override void OnLoad()
        {
        }

        protected override void OnUnload()
        {
        }

        private static void ILPatch_GlobalEventManager_OnHitEnemy_TriTipChance(ILContext il)
        {
            ILCursor csr = new ILCursor(il);
            //Starting at DnSpy's IL View: Line 894: "/* 0x0003CCB0 60 */ IL_0148: or"
            csr.GotoNext(
                x => x.MatchOr(),
                x => x.MatchBrfalse(out var IL_0195),
                x => x.MatchLdloc(out int V_23),
                x => x.MatchBrtrue(out var IL_0167),
                x => x.MatchLdcR4(15)   //Target: Tri-Tip Bleed Chance
                );

            csr.Index += 4;
            csr.Next.Operand = BleedConfig.Dagger_ProcChance.Value;
        }

        private static void ILPatch_GlobalEventManager_OnHitEnemy_BleedTime(ILContext il)
        {
            ILCursor csr = new ILCursor(il);
            //Starting at DnSpy's IL View: Line 908: "/* 0x0003CCC8 287F220006   */ IL_0160: call  bool RoR2.Util::CheckRoll(float32, class RoR2.CharacterMaster)"
            csr.GotoNext(
                i => i.MatchCallOrCallvirt("RoR2.Util", "CheckRoll"),
                i => i.MatchBrfalse(out var IL_0BF1),
                i => i.MatchLdarg(1),
                i => i.MatchLdfld<RoR2.DamageInfo>("procChainMask"),
                i => i.MatchStloc(out int V_24),
                i => i.MatchLdloca(out int V_24),
                i => i.MatchLdcI4(5),
                i => i.MatchCallOrCallvirt<RoR2.ProcChainMask>("AddProc"),
                i => i.MatchLdarg(2),
                i => i.MatchLdarg(1),
                i => i.MatchLdfld<RoR2.DamageInfo>("attacker"),
                i => i.MatchLdcI4(0),
                i => i.MatchLdcR4(3)    //Bleed time
                );

            csr.Index += 12;
            csr.Next.Operand = BleedConfig.StandardBleed_TimeSecs.Value;

            //Shatterspleen Bleed Time
            //Starting at DnSpy's IL View: Line 1966: "/* 0x0003D724 7B13060004 */ IL_0BBC: ldfld"
            csr.GotoNext(
                i => i.MatchLdfld<RoR2.DamageInfo>("crit"),
                i => i.MatchBrfalse(out var IL_0BF1),
                i => i.MatchLdarg(1),
                i => i.MatchLdfld<RoR2.DamageInfo>("procChainMask"),
                i => i.MatchStloc(out int V_72),
                i => i.MatchLdloca(out int V_72),
                i => i.MatchLdcI4(5),
                i => i.MatchCallOrCallvirt<RoR2.ProcChainMask>("AddProc"),
                i => i.MatchLdarg(2),
                i => i.MatchLdarg(1),
                i => i.MatchLdfld<RoR2.DamageInfo>("attacker"),
                i => i.MatchLdcI4(0),
                i => i.MatchLdcR4(3)    //Bleed time
                );

            csr.Index += 12;
            csr.Next.Operand = BleedConfig.ShatterspleenBleed_TimeSecs.Value;
        }
    }
}
