using RoR2QoLChanges.Configuration.Items;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Patches.Items
{
    public class WarbannerChanges: MonoModPatchable
    {
        public WarbannerConfig activeConfig;

        public override void ApplyPatches()
        {
            if (!activeConfig.Enabled.Value)
                return;

            On.RoR2.CharacterBody.RecalculateStats += ApplyWarbannerChanges;
        }

        private void ApplyWarbannerChanges(On.RoR2.CharacterBody.orig_RecalculateStats orig, RoR2.CharacterBody self)
        {
            orig(self);

            if (!self.HasBuff(RoR2.BuffIndex.Warbanner))
                return;

            float stacks = PluginCore.WarbannerBuffHelper.GetWarbannerAttackSpeedModifier(self.corePosition);
            float atkSpeed = self.attackSpeed;
            float charAS = self.baseAttackSpeed + self.levelAttackSpeed * self.level;

            atkSpeed /= charAS; //get the modifier only values
            atkSpeed -= 0.3f;   //remove original warbanner value;
            atkSpeed += activeConfig.attackSpeedBase.Value + (stacks * activeConfig.attackSpeedPerStack.Value);
            atkSpeed *= charAS;

            self.attackSpeed = atkSpeed; 
        }

        public WarbannerChanges(WarbannerConfig config) => activeConfig = config;
    }
}
