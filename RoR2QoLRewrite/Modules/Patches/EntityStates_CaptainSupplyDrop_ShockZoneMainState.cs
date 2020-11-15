using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Modules.Patches
{
    static class EntityStates_CaptainSupplyDrop_ShockZoneMainState
    {
        static bool LOADED = false;

        public static System.Action<EntityStates.CaptainSupplyDrop.ShockZoneMainState> Pre_OnEnter, Post_OnEnter;

        static void Load()
        {
            if (LOADED)
                Unload();

            On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.OnEnter += OnEnter;

            LOADED = true;
        }

        static void Unload()
        {
            On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.OnEnter -= OnEnter;

            LOADED = false;
        }

        static void OnEnter(On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.orig_OnEnter orig, EntityStates.CaptainSupplyDrop.ShockZoneMainState self)
        {
            Pre_OnEnter(self);
            orig(self);
            Post_OnEnter(self);
        }
    }
}
