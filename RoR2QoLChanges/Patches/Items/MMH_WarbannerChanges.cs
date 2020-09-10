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
            orig(self);

            UnityEngine.Debug.LogWarning($"Warbanner Buffstate = {self.HasBuff(RoR2.BuffIndex.Warbanner)}");

            if (!self.HasBuff(RoR2.BuffIndex.Warbanner))
                return;

            float stacks = PluginCore.WarbannerBuffHelper.GetWarbannerAttackSpeedModifier(self.corePosition);
            UnityEngine.Debug.LogWarning($"WarbannerStacks Count = {stacks}");
            float atkSpeed = self.attackSpeed;
            float charAS = self.baseAttackSpeed + self.levelAttackSpeed * self.level;

            atkSpeed /= charAS; //get the modifier only values
            atkSpeed -= 0.3f;   //remove original warbanner value;
            atkSpeed += activeConfig.attackSpeedBase.Value + (stacks * activeConfig.attackSpeedPerStack.Value);
            atkSpeed *= charAS;

            self.attackSpeed = atkSpeed; 
        }

        public MMH_WarbannerChanges(WarbannerConfig config) => activeConfig = config;
    }
}
