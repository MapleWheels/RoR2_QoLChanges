using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2QoLChanges.Additions.Mechanics
{
    [RequireComponent(typeof(CharacterBody))]
    [DisallowMultipleComponent]
    public class MinionOnKillProcBehaviour : MonoBehaviour
    {
        public float ChanceToPassOnHit = 100f;
        public CharacterBody selfBody;
        public CharacterMaster ownerMaster;

        void Start()
        {
            selfBody = GetComponent<CharacterBody>();
            ownerMaster = selfBody.master.minionOwnership.ownerMaster;
        }

        public void ProcessAttackerOnKillEffects(DamageReport report)
        {
            if (ownerMaster)
            {
                UnityEngine.Debug.LogWarning($"MinionOnKill..::ProcessAttackerOnKillEffects()|ownermaster={ownerMaster}");

                if (Util.CheckRoll(ChanceToPassOnHit, 0, null))
                {
                    UnityEngine.Debug.LogWarning($"MinionOnKill..::ProcessAttackerOnKillEffects()|untilCheckRoll Good.");

                    DamageReport reportClone = new DamageReport(report.damageInfo, report.victim, report.damageDealt, report.combinedHealthBeforeDamage);
                    reportClone.attacker = ownerMaster.gameObject;
                    reportClone.attackerBody = ownerMaster.GetBody();
                    reportClone.attackerBodyIndex = BodyCatalog.FindBodyIndex(reportClone.attackerBody);
                    reportClone.attackerMaster = ownerMaster;
                    reportClone.attackerTeamIndex = ownerMaster.teamIndex;

                    GlobalEventManager.instance.OnCharacterDeath(reportClone);
                }
            }
        }
    }
}
