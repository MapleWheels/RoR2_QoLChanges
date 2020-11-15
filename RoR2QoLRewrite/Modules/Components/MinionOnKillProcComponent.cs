using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RoR2;

using UnityEngine;

namespace RoR2QoLRewrite.Modules.Components
{
    [DisallowMultipleComponent]
    public sealed class MinionOnKillProcComponent : MonoBehaviour
    {
        public float OnKillProcChance = 10f;
    }
}
