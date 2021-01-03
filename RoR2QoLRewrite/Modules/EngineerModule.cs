using IL.RoR2;
using RoR2QoLRewrite.Components;
using RoR2QoLRewrite.Configuration.Survivors;
using RoR2QoLRewrite.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoR2QoLRewrite.Modules
{
    internal class EngineerModule : ModuleBase
    {
        private const string EngiTurretPrefabName = "prefabs/characterbodies/EngiTurretBody";
        private const string EngiWalkerTurretPrefabName = "prefabs/characterbodies/EngiWalkerTurretBody";
        private float EngiPainterVanillaMaxDistance;

        protected override void OnDisable()
        {
            GameObject.Destroy(Cache<TagMinionOnKillProc>.Dispose(EngiTurretPrefabName));
            GameObject.Destroy(Cache<TagMinionOnKillProc>.Dispose(EngiWalkerTurretPrefabName));

            On.RoR2.CharacterBody.RecalculateStats -= CharacterBody_RecalculateStats;
            RoR2.GlobalEventManager.onCharacterDeathGlobal -= GlobalEventManager_onCharacterDeathGlobal;

            EntityStates.Engi.EngiMissilePainter.Paint.maxDistance = EngiPainterVanillaMaxDistance; //Restore vanilla
        }

        protected override void OnEnable()
        {
            if (EngineerConfig.EngiTurretOnKillProcEnabled)
            {
                Cache<TagMinionOnKillProc>.Add(EngiTurretPrefabName, Cache<GameObject>.Get(EngiTurretPrefabName).AddComponent<TagMinionOnKillProc>());
                Cache<TagMinionOnKillProc>.Add(EngiWalkerTurretPrefabName, Cache<GameObject>.Get(EngiWalkerTurretPrefabName).AddComponent<TagMinionOnKillProc>());
            }

            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            RoR2.GlobalEventManager.onCharacterDeathGlobal += GlobalEventManager_onCharacterDeathGlobal;

            EngiPainterVanillaMaxDistance = EntityStates.Engi.EngiMissilePainter.Paint.maxDistance; //Cache
            EntityStates.Engi.EngiMissilePainter.Paint.maxDistance = EngineerConfig.EngiMissilePainer_MaxDistance;
        }

        private void GlobalEventManager_onCharacterDeathGlobal(RoR2.DamageReport damageReport)
        {
            if (!damageReport.attacker || !damageReport.damageInfo.inflictor || damageReport.attacker == damageReport.victim || damageReport.damageInfo.inflictor == damageReport.victim)
                return;

            if (damageReport.attacker.GetComponent<TagMinionOnKillProc>())
                OnMinionKillProc(damageReport, EngineerConfig.EngiTurret_ChanceOnKillProcAppliedToEngi);
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, RoR2.CharacterBody self)
        {
            orig(self);
            if (self.name.ToLower().Trim().Contains("EngiWalkerTurret"))
            {
                self.moveSpeed *= self.sprintingSpeedMultiplier;
            }
        }

        protected override void OnLoad()
        {
            Cache<GameObject>.Add(EngiTurretPrefabName, Resources.Load<GameObject>(EngiTurretPrefabName));
            Cache<GameObject>.Add(EngiWalkerTurretPrefabName, Resources.Load<GameObject>(EngiWalkerTurretPrefabName));
        }

        protected override void OnUnload()
        {
            GameObject.Destroy(Cache<GameObject>.Dispose(EngiTurretPrefabName));
            GameObject.Destroy(Cache<GameObject>.Dispose(EngiWalkerTurretPrefabName));
        }

        internal static void OnMinionKillProc(RoR2.DamageReport report, float onHitProcChance)
        {
            var selfBody = report.attackerBody;
            var ownerBody = report.attackerOwnerMaster.GetBody();

            if (!selfBody)
                return;

            if (!ownerBody || ownerBody == selfBody)
                return;

            if (RoR2.Util.CheckRoll(onHitProcChance, ownerBody.master))
            {
                report.attacker = ownerBody.gameObject;
                report.attackerBody = ownerBody;
                report.attackerBodyIndex = RoR2.BodyCatalog.FindBodyIndex(report.attackerBody);
                report.attackerMaster = ownerBody.master;
                report.attackerTeamIndex = ownerBody.master.teamIndex;
                report.damageInfo.attacker = ownerBody.gameObject;
                report.damageInfo.inflictor = ownerBody.gameObject;

                RoR2.GlobalEventManager.instance.OnCharacterDeath(report);
            }
        }
    }
}
