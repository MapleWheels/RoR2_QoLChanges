using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RoR2;

namespace RoR2QoLRewrite.Modules.Components
{
	[RequireComponent(typeof(RoR2.HealingWard))]
	[DisallowMultipleComponent]
	public class MissingHpBoostBuffWardComponent : BuffWard
    {
		void Start()
        {
			base.Start();
        }
	}
}
