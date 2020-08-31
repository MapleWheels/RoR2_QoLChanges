using EntityStates.LunarTeleporter;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2QoLChanges.Configuration;
using RoR2QoLChanges.Configuration.Mechanics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Patches.Mechanics
{
    public class MMH_BleedChanges : MonoModPatchable
    {
        protected BleedConfig activeConfig;

        public MMH_BleedChanges(BleedConfig config) => activeConfig = config;

        public override void ApplyPatches()
        {
            Patch_DotController_InitDotCatalog();
            Patch_GlobalEventManager_OnHitEnemy();
        }

        private void Patch_GlobalEventManager_OnHitEnemy()
        {
            //Tri-tip Proc Chance
            IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
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
                csr.Next.Operand = activeConfig.Dagger_ProcChance.Value;
            };

            //Tri-tip Bleed Time
            IL.RoR2.GlobalEventManager.OnHitEnemy += (il) =>
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
                csr.Next.Operand = activeConfig.StandardBleed_TimeSecs.Value;

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
                csr.Next.Operand = activeConfig.ShatterspleenBleed_TimeSecs.Value;
            };
        }

        private void Patch_DotController_InitDotCatalog()
        {
            IL.RoR2.DotController.InitDotCatalog += (il) =>
            {
                ILCursor csr = new ILCursor(il);
                //IL Match
                csr.GotoNext(
                    x => x.MatchDup(),
                    x => x.MatchLdcR4(0.25f),
                    x => x.MatchStfld<RoR2.DotController.DotDef>("interval"),
                    x => x.MatchDup(),
                    x => x.MatchLdcR4(0.2f),    //Target value, Bleeding Damage Coefficient per interval.
                    x => x.MatchStfld<RoR2.DotController.DotDef>("damageCoefficient")
                    );

                csr.Index += 1;
                csr.Next.Operand = BleedConfig.BleedIntervalRate;

                csr.Index += 3;
                csr.Next.Operand = activeConfig.Bleed_BaseDamageRatio.Value * BleedConfig.BleedIntervalRate / activeConfig.StandardBleed_TimeSecs.Value;
            };
        }
    }
}
