using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules.Patches
{
    static class Ror2_CharacterBody
    {
        static bool _LOADED = false;

        public static System.Action<RoR2.CharacterBody> Pre_RecalculateStats, Post_RecalculateStats;

        public static void Load()
        {
            if (_LOADED)
                Unload();

            On.RoR2.CharacterBody.RecalculateStats += Ror2_CharacterBody.RecalculateStats;

            _LOADED = true;

        }

        public static void Unload()
        {
            On.RoR2.CharacterBody.RecalculateStats -= Ror2_CharacterBody.RecalculateStats;

            _LOADED = false;
        }

        static void RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, RoR2.CharacterBody self)
        {
            Pre_RecalculateStats?.Invoke(self);
            orig(self);
            Post_RecalculateStats?.Invoke(self);
        }
    }
}
