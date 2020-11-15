using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules.Patches
{
    static class RoR2_GlobalEventManager
    {
        static bool LOADED = false;

        internal static System.Action<RoR2.GlobalEventManager, RoR2.DamageReport> Pre_OnCharacterDeath, Post_OnCharacterDeath;

        internal static void Load()
        {
            if (LOADED)
                Unload();

            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            LOADED = true;
        }

        internal static void Unload()
        {
            On.RoR2.GlobalEventManager.OnCharacterDeath -= GlobalEventManager_OnCharacterDeath;

            LOADED = false;
        }

        private static void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, RoR2.GlobalEventManager self, RoR2.DamageReport damageReport)
        {
            Pre_OnCharacterDeath?.Invoke(self, damageReport);
            orig(self, damageReport);
            Post_OnCharacterDeath?.Invoke(self, damageReport);
        }
    }
}
