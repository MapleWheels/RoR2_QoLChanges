using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using RoR2;
using R2API;
using UnityEngine.Networking;
using RoR2QoLRewrite.Modules.Networking;
using R2API.Networking.Interfaces;

namespace RoR2QoLRewrite.Modules.Components
{
    [DisallowMultipleComponent]
    public class WarbannerHelperComponent : MonoBehaviour
    {
        public NetworkInstanceId WardInstanceId;
        public BuffWard AttachedWard;
        public float NetworkRadius;
        public float StackCount;
        public static float RadiusPerStack = 8f;

        protected bool IsReady;

        void Start()
        {
            if (!AttachedWard)
                AttachedWard = GetComponent<BuffWard>();

            if (!AttachedWard)
                IsReady = false;

            WardInstanceId = GetComponent<NetworkIdentity>().netId;

            if (!NetworkServer.active)
                IsUpdated = true;

            NextUpdateTime = Time.time + LateUpdateDelay;
        }

        protected bool IsUpdated = false;
        protected const float LateUpdateDelay = 2f;
        protected float NextUpdateTime;

        void Update()
        {
            if (!IsReady || IsUpdated || Time.time < NextUpdateTime)
                return;

            NetworkRadius = AttachedWard.Networkradius;
            StackCount = (NetworkRadius - RadiusPerStack) / RadiusPerStack; //Reverse engineer Warbanner range formula to find stack count

            new WarbannerSyncMsg(NetworkRadius, WardInstanceId).Send(R2API.Networking.NetworkDestination.Clients);

            IsUpdated = true;
        }
    }
}
