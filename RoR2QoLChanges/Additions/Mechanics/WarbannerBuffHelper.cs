using RoR2QoLChanges.Configuration.Items;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2QoLChanges.Additions.Mechanics
{
    public class WarbannerBuffHelper
    {
        public List<WarbannerBuffRpcBehaviour> activeWards { get; protected set; }
        public float GetWarbannerAttackSpeedModifier(Vector3 selfPosition)
        {
            //Clean the list
            activeWards.RemoveAll(i => !i || !i.IsReady);

            float count = 0f;
            foreach(WarbannerBuffRpcBehaviour ward in activeWards)
            {
                float wardSqrDist = ward.AttachedWard.calculatedRadius * ward.AttachedWard.calculatedRadius;

                if ((selfPosition - ward.transform.position).sqrMagnitude <= wardSqrDist)
                    count += ward.WarbannerStacks;
            }
            return count;
        }

        public void RegisterWard(WarbannerBuffRpcBehaviour ward)
        {
            List<WarbannerBuffRpcBehaviour> existingWard = activeWards.FindAll(x => x == ward);
            if (existingWard.Count > 0)
            {
                UnityEngine.Debug.LogWarning($"Warning: WarbannerBuffWard={ward} tried adding itself twice to the Warbanner helper.");
                return;
            }
            activeWards.Add(ward);
        }

        protected void Init()
        {
            activeWards = new List<WarbannerBuffRpcBehaviour>();
        }

        public WarbannerBuffHelper() => Init();
    }
}
