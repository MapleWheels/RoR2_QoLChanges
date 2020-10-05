using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using MiniRpcLib;
using MiniRpcLib.Action;

using RoR2;

using RoR2QoLChanges.Additions.Networking;

using UnityEngine;
using UnityEngine.Networking;

namespace RoR2QoLChanges.Additions.Mechanics
{
    [DisallowMultipleComponent]
    public class WarbannerBuffRpcBehaviour : MonoBehaviour
    {
        public void RpcSetWarbannerStackCountServer(short count)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError($"[Host] WarbannerBuff..Behaviour::RpcSetWarbannerStackCountServer() Called on Client! GameObject={gameObject.name}");
                return;
            }
            if (!IsReady)
            {
                Debug.LogError($"[Host] WarbannerBuff..Behaviour::RpcSetWarbannerStackCountServer() | Not Ready! GameObject={gameObject.name}");
                return;
            }

            WarbannerStacks = count;
            SendWarbannerStackDataToClients.Invoke(count);
        }

        protected void RpcRegisterWarbannerBuffBehaviourClient(NetworkUser user)
        {
            if (!this.IsReady)
            {
                Debug.LogError($"[Client] WarbannerBuff..Behaviour::RpcReceiveWarbannerStackInfo() | Not Ready! GameObject={gameObject.name}");
                return;
            }
            PluginCore.WarbannerBuffHelper.RegisterWard(this);
        }

        protected void RegisterWarBannerWardServer()
        {
            if (!NetworkServer.active)
            {
                Debug.LogError($"[Host] WarbannerBuff..Behaviour::RegisterWarBannerWardServer() Called on Client! GameObject={gameObject.name}");
                return;
            }
            PluginCore.WarbannerBuffHelper.RegisterWard(this);
            RegisterWarbannerBuffBehavioursOnClients.Invoke(false);
        }

        protected virtual void RpcReceiveWarbannerStackCountClient(NetworkUser user, short stackCount)
        {
            if (!IsReady)
            {
                Debug.LogError($"[Client] WarbannerBuff..Behaviour::RpcReceiveWarbannerStackInfo() | Not Ready! GameObject={gameObject.name}");
                return;
            }
            WarbannerStacks = stackCount;
        }

        protected void Initialize()
        {
            MiniRpcInstance = MiniRpcManager.Instance.MiniRpcInstance;
            AttachedWard = GetComponent<BuffWard>();
            InitializeRpcActions();
        }

        protected virtual void InitializeRpcActions()
        {
            SendWarbannerStackDataToClients = MiniRpcInstance.RegisterAction(
                Target.Client,
                (Action<NetworkUser, short>)Delegate.CreateDelegate(typeof(Action<NetworkUser, short>), this, nameof(RpcReceiveWarbannerStackCountClient))
                );
            RegisterWarbannerBuffBehavioursOnClients = MiniRpcInstance.RegisterAction(
                Target.Client,
                (NetworkUser user, bool _) => { RpcRegisterWarbannerBuffBehaviourClient(user); }
                );
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Unity Message")]
        public void Start()
        {
            Initialize();
            if (IsReady && NetworkServer.active)
            {
                short stackCount = (short)((AttachedWard.Networkradius - 8f) / 8f); //Reverse engineer radius formula to find stack count.
                RegisterWarBannerWardServer();
                RpcSetWarbannerStackCountServer(stackCount);
            }
        }

        public void Update()
        {
            if (Time.time > nextUpdateTime)
            {
                nextUpdateTime = Time.time + updateInterval;
                short stackCount = (short)((AttachedWard.Networkradius - 8f) / 8f); //Reverse engineer radius formula to find stack count.
                if (NetworkServer.active)
                    RpcSetWarbannerStackCountServer(stackCount);
            }
        }

        protected static float updateInterval = 3f;
        protected float nextUpdateTime;
        protected MiniRpcInstance MiniRpcInstance { get; set; }
        public short WarbannerStacks { get; protected set; }
        protected IRpcAction<short> SendWarbannerStackDataToClients { get; set; }
        protected IRpcAction<bool> RegisterWarbannerBuffBehavioursOnClients { get; set; }
        public virtual bool IsReady { get => AttachedWard; }
        public BuffWard AttachedWard { get; protected set; }
    }
}
