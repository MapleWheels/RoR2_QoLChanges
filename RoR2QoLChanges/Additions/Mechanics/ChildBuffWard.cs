using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using MiniRpcLib;
using MiniRpcLib.Action;

using RoR2;

using RoR2QoLChanges.Additions.Buffs;
using RoR2QoLChanges.Additions.Networking;

using UnityEngine;
using UnityEngine.Networking;

namespace RoR2QoLChanges.Additions.Mechanics
{
    public class ChildBuffWard : BuffWard 
    {
        public Transform ParentWard;
        public bool MatchParentNetworkRadius;
        protected BuffWard TargetWard;
        void Start()
        {
            base.Start();
            if (MatchParentNetworkRadius)
            {
                if (ParentWard)
                {
                    TargetWard = ParentWard.GetComponent<BuffWard>();
                    base.Networkradius = TargetWard ? TargetWard.Networkradius : 0;
                    base.needsRemovalTime = false;
                }
                else
                {
                    UnityEngine.Debug.LogError("Could not get parent BuffWard.");
                }
            }
        }
    }
}
