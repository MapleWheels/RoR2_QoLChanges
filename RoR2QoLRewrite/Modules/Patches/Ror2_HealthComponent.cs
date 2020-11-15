using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules.Patches
{
    static class Ror2_HealthComponent
    {
        static bool _LOADED = false;

        public static System.Func<RoR2.HealthComponent, float, RoR2.ProcChainMask, bool, float> Pre_Heal, Post_Heal;

        public static void Load()
        {
            if (_LOADED)
                Unload();

            On.RoR2.HealthComponent.Heal += Ror2_HealthComponent.Heal;
            _LOADED = true;

        }

        public static void Unload()
        {
            On.RoR2.HealthComponent.Heal -= Ror2_HealthComponent.Heal;

            _LOADED = false;
        }

        static float Heal(On.RoR2.HealthComponent.orig_Heal orig, RoR2.HealthComponent self, float amount, RoR2.ProcChainMask procChainMask, bool nonRegen)
        {
            float? amount2 = Pre_Heal?.Invoke(self, amount, procChainMask, nonRegen);

            if (amount2 == null)
                amount2 = amount;

            amount = orig(self, (float)amount2, procChainMask, nonRegen);

            amount2 = Post_Heal?.Invoke(self, amount, procChainMask, nonRegen);

            if (amount2 == null)
                return amount;
            else
                return (float)amount2;
        }
    }
}
