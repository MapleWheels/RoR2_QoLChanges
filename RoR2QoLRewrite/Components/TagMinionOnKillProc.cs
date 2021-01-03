using RoR2QoLRewrite.Configuration.Survivors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoR2QoLRewrite.Components
{
    /// <summary>
    /// TAG MB. Any minions with this Tag will have a chance to apply their on kill effects to their owner-master.
    /// </summary>
    [DisallowMultipleComponent]
    public class TagMinionOnKillProc : MonoBehaviour { }
}
