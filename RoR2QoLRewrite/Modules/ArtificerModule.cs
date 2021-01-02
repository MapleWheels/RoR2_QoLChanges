using RoR2QoLRewrite.Configuration.Survivors;
using BepInEx.Extensions.Configuration;
using RoR2QoLRewrite.Util;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoR2;
using UnityEngine;

namespace RoR2QoLRewrite.Modules
{
    internal class ArtificerModule : ModuleBase
    {
        protected override void OnDisable()
        {
            On.RoR2.CharacterBody.RecalculateStats -= CharacterBody_RecalculateStats;
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);
            CharacterBody_RecalculateStats(self);
        }

        protected override void OnEnable()
        {
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
        }

        protected override void OnLoad()
        {
        }

        protected override void OnUnload()
        {
        }

        private void CharacterBody_RecalculateStats(RoR2.CharacterBody body)
        {
            if (body.name.ToLower().Contains(ArtificerConfig.ArtificerBodyName.ToLower()))
            {
                GenericSkill m1 = body.skillLocator.primary;

                m1.SetBonusStockFromBody(
                    Math.Min(
                        (int)(body.level / ArtificerConfig.M1Fire_LevelsPerCharge.Value),
                        ArtificerConfig.M1Fire_MaxLevelCharges.Value
                        ) +
                    Math.Min(
                        (body.inventory.GetItemCount(ItemIndex.SecondarySkillMagazine) / ArtificerConfig.M1Fire_BackupMagsPerCharge.Value),
                        ArtificerConfig.M1Fire_MaxBackupMagCharges.Value
                        )
                    );

                m1.cooldownScale *= 1f - Mathf.Min(
                    Mathf.Max(
                        body.attackSpeed - (body.baseAttackSpeed + body.levelAttackSpeed * body.level), 0f)
                    * ArtificerConfig.M1Fire_CooldownPerAttackSpeedRatio.Value,
                    1f - ArtificerConfig.M1Fire_MaxCooldownFromAttackSpeed.Value
                    );
            }
        }
    }
}
