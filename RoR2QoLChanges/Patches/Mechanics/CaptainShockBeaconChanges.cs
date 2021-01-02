using RoR2QoLChanges.Additions.Mechanics;
using RoR2QoLChanges.Configuration.Survivors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Patches.Mechanics
{
    public class CaptainShockBeaconChanges : MonoModPatchable
    {
        protected CaptainConfig ActiveConfig;
        public CaptainShockBeaconChanges(CaptainConfig config) => this.ActiveConfig = config;

        public override void ApplyPatches()
        {
            if (!ActiveConfig.Enabled.Value)
                return;

            On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.OnEnter += ShockZoneMainState_OnEnter;
        }

        private void ShockZoneMainState_OnEnter(On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.orig_OnEnter orig, EntityStates.CaptainSupplyDrop.ShockZoneMainState self)
        {
            orig(self);
            WardCleanseEffectBehaviour component = self.gameObject.GetComponent<WardCleanseEffectBehaviour>();
            if (!component)
                component = self.gameObject.AddComponent<WardCleanseEffectBehaviour>();

            component.NetworkRadius = ActiveConfig.Beacon_ShockDefaultRadius.Value;
        }

        public override void RemovePatches()
        {
            On.EntityStates.CaptainSupplyDrop.ShockZoneMainState.OnEnter -= ShockZoneMainState_OnEnter;
        }
    }
}
