using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2QoLChanges.Additions.Mechanics
{
    [DisallowMultipleComponent]
    public class SquidPolypConfiguratorBehaviour : MonoBehaviour
    {
        public CharacterBody squidBody;

        void Start()
        {
            InitAndRemoveHealthDecay();
        }

        void InitAndRemoveHealthDecay()
        {
            squidBody = GetComponent<CharacterBody>();
            //Remove the health decay from SquipPolyp.
            if (squidBody)
            {
                int num = squidBody.inventory.GetItemCount(ItemIndex.HealthDecay);
                squidBody.inventory.RemoveItem(ItemIndex.HealthDecay, num);
            }
        }
    }
}
