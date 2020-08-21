using HarmonyLib;

using RoR2_QoLChanges.Configuration;
using RoR2QoLChanges.Patches;
using RoR2QoLChanges.Patches.Items;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2_QoLChanges.Patches.Items
{
    public class HI_BleedChanges : HarmonyPatchable
    {
        public static BleedConfig activeConfig { get; protected set; }

        public HI_BleedChanges(Harmony instance, BleedConfig activeConfig) : base(instance) => HI_BleedChanges.activeConfig = activeConfig;
    }
}
