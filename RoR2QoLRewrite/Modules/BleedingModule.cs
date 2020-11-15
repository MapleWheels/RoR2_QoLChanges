using BepInEx.Configuration;
using BepInEx.Extensions.Configuration;
using BepInEx.Logging;
using RoR2QoLRewrite.Configuration.Mechanics;

using MonoMod.Cil;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules
{
    class BleedingModule : IModule
    {
        private static BleedConfig Config;

        public bool IsEnabled { get; private set; }
        public bool IsLoaded { get; private set; }

        public void DisableModule()
        {
            if (!IsEnabled)
                return;
            BleedPatch.Unpatch();
            IsEnabled = false;
        }

        public void EnableModule()
        {
            if (IsEnabled || !IsLoaded)
                return;
            BleedPatch.Patch();
            IsEnabled = true;
        }

        public void LoadModule(ConfigFile file, ManualLogSource logger)
        {
            if (IsLoaded)
                return;
            Config = file.BindModel<BleedConfig>(logger);
            IsLoaded = true;

            if (Config.Enabled)
                EnableModule();
        }

        public void SetConfig(ConfigFile file)
        {
            if (!IsLoaded)
                return;
            Config.SetConfigFile(file);
        }

        public void UnloadModule()
        {
            if (!IsLoaded)
                return;
            if (IsEnabled)
                DisableModule();
            IsLoaded = false;
        }

        static class BleedPatch
        {
            static bool PATCH_ACTIVE = false;
            internal static float
                Dagger_ProcChance = 7f,
                StandardBleedDebuffTime = 4f,
                ShatterspleenBleedDebuffTime = 2f,
                BleedIntervalRate = 0.25f,
                BleedBaseDamageRatio = 1.8f;

            internal static void Patch() {
                if (PATCH_ACTIVE)
                    Unpatch();

                IL.RoR2.GlobalEventManager.OnHitEnemy += ILPatch_GlobalEventManager_OnHitEnemy_TriTipProcChance;
                IL.RoR2.GlobalEventManager.OnHitEnemy += ILPatch_GlobalEventManager_OnHitEnemy_BleedDebuffTime;

                SetVal_DotController_InitDotCatalog();

                PATCH_ACTIVE = true;
            }

            internal static void Unpatch() 
            {
                IL.RoR2.GlobalEventManager.OnHitEnemy -= ILPatch_GlobalEventManager_OnHitEnemy_TriTipProcChance;
                IL.RoR2.GlobalEventManager.OnHitEnemy -= ILPatch_GlobalEventManager_OnHitEnemy_BleedDebuffTime;

                PATCH_ACTIVE = false;
            }

            static void ILPatch_GlobalEventManager_OnHitEnemy_TriTipProcChance(ILContext il)
            {
                //Tri-tip Proc Chance
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
                csr.Next.Operand = Dagger_ProcChance;
            }

            static void ILPatch_GlobalEventManager_OnHitEnemy_BleedDebuffTime(ILContext il)
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
                csr.Next.Operand = StandardBleedDebuffTime;

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
                csr.Next.Operand = ShatterspleenBleedDebuffTime;
            }

            static void SetVal_DotController_InitDotCatalog()
            {
                RoR2.DotController.dotDefs[0].interval = BleedIntervalRate;
                RoR2.DotController.dotDefs[0].damageCoefficient = BleedBaseDamageRatio * BleedIntervalRate / StandardBleedDebuffTime;
            }
        }
    }
}
