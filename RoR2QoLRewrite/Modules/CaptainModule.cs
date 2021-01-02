using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx.Extensions.Configuration;
using RoR2QoLRewrite.Configuration.Survivors;

namespace RoR2QoLRewrite.Modules
{
    internal class CaptainModule : ModuleBase
    {
        internal static CaptainConfig ActiveConfig;

        protected override void OnDisable()
        {
            On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.OnEnter -= ShockZoneMainState_OnEnter;
        }

        protected override void OnEnable()
        {
            On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.OnEnter += ShockZoneMainState_OnEnter;
            On.EntityStates.CaptainSupplyDrop.HealZoneMainState.OnEnter += HealZoneMainState_OnEnter;
        }

        private void HealZoneMainState_OnEnter(On.EntityStates.CaptainSupplyDrop.HealZoneMainState.orig_OnEnter orig, EntityStates.CaptainSupplyDrop.HealZoneMainState self)
        {
            orig(self);
        }

        private void ShockZoneMainState_OnEnter(On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.orig_OnEnter orig, EntityStates.CaptainSupplyDrop.ShockZoneMainState self)
        {
            orig(self);
        }

        protected override void OnLoad()
        {

        }

        protected override void OnUnload()
        {

        }
    }
}
