using RoR2;
using R2API.Networking.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using RoR2QoLRewrite.Modules.Components;

namespace RoR2QoLRewrite.Modules.Networking
{
    public class WarbannerSyncMsg : INetMessage
    {
        public NetworkInstanceId WarbannerInstanceId;
        public float BannerRadius;

        public void OnReceived()
        {
            if (NetworkServer.active)
                return;

            GameObject warbannerWard = Util.FindNetworkObject(WarbannerInstanceId);
            if (!warbannerWard)
            {
                PluginCoreModule.Logger.LogError("WarbannerSyncMsg::OnReceived() | WarbannerWard is null");
                return;
            }
            
            Components.WarbannerHelperComponent component = warbannerWard.GetComponent<WarbannerHelperComponent>();
            if (!component)
            {
                PluginCoreModule.Logger.LogError("WarbannerSyncMsg::OnReceived() | WarbannerWardHelper component is null");
                return;
            }

            if (component.WardInstanceId != WarbannerInstanceId)
            {
                PluginCoreModule.Logger.LogError("WarbannerSyncMsg::OnReceived() | Warbanner Instance Ids are different.");
                return;
            }

            component.NetworkRadius = this.BannerRadius;
            component.StackCount = (this.BannerRadius - WarbannerHelperComponent.RadiusPerStack) / WarbannerHelperComponent.RadiusPerStack;

        }

        public void Deserialize(NetworkReader reader)
        {
            WarbannerInstanceId = reader.ReadNetworkId();
            BannerRadius = reader.ReadSingle();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(WarbannerInstanceId);
            writer.Write(BannerRadius);
        }

        public WarbannerSyncMsg(float bannerRadius, NetworkInstanceId warbannerNetId)
        {
            this.BannerRadius = bannerRadius;
            this.WarbannerInstanceId = warbannerNetId;
        }

        public WarbannerSyncMsg() { }
    }
}
