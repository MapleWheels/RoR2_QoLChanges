using RoR2QoLChanges.Configuration.Items;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Patches.Items
{
    public class MMH_WarbannerChanges: MonoModPatchable
    {
        public WarbannerConfig activeConfig;

        public override void ApplyPatches()
        {
            On.RoR2.CharacterBody.RecalculateStats += ApplyWarbannerChanges;
        }

        private void ApplyWarbannerChanges(On.RoR2.CharacterBody.orig_RecalculateStats orig, RoR2.CharacterBody self)
        {

        }

        public MMH_WarbannerChanges(WarbannerConfig config) => activeConfig = config;
    }
}
