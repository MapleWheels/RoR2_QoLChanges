using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoR2QoLRewrite.Modules.Components
{
    public class SquidPolypConfiguratorComponent : MonoBehaviour
    {
        public CharacterBody squidBody;
        public float DecayAdjustRatio = 0f;

        void Start()
        {
            AdjustHealthDecayRate();
        }

        void AdjustHealthDecayRate()
        {
            squidBody = GetComponent<CharacterBody>();
            //Adjust the health decay from SquipPolyp.
            if (squidBody)
            {
                int num = squidBody.inventory.GetItemCount(ItemIndex.HealthDecay);
                int num2 = Mathf.RoundToInt(Mathf.Max(num * DecayAdjustRatio, 0f));

                if (DecayAdjustRatio <= 1f)
                    squidBody.inventory.RemoveItem(ItemIndex.HealthDecay, num - num2);
                else
                    squidBody.inventory.GiveItem(ItemIndex.HealthDecay, num2 - num);
                }
            }
        }
    }
}
