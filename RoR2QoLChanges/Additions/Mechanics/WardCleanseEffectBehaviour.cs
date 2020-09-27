using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Extensions;
using R2API;
using RoR2;
using UnityEngine;

namespace RoR2QoLChanges.Additions.Mechanics
{
    [DisallowMultipleComponent]
    public class WardCleanseEffectBehaviour : MonoBehaviour
    {
        public TeamFilter teamFilter;

        void Start()
        {

        }

        void Update()
        {
            if (Time.deltaTime > NextUpdateTime)
            {
                NextUpdateTime += UpdateInterval;
                //logic here
            }
        }

        private float NextUpdateTime;
        public float NetworkRadius;
        public static float UpdateInterval = 0.25f;
    }
}
