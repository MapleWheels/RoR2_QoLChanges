using EntityStates.LunarTeleporter;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2_QoLChanges.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2_QoLChanges.Patches.Mechanics
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
                    x => x.MatchStfld<float>("RoR2.DotController/DotDef::interval"),
                    x => x.MatchDup(),
                    x => x.MatchLdcR4(0.2f),    //Target value, Bleeding Damage Coefficient per second.
                    x => x.MatchStfld<float>("RoR2.DotController/DotDef::damageCoefficient")
                    );

                csr.Index += 4;
                csr.Next.Operand = activeConfig.Bleed_BaseDamageRatio.Value * 0.25f * activeConfig.StandardBleed_TimeSecs.Value;
            };
        }
    }
}
