using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2QoLChanges.Additions.Mechanics
{
    [DisallowMultipleComponent]
    public class MinionOnKillProcBehaviour : MonoBehaviour
    {
        public float ChanceToPassOnHit = 10f;

        public void ProcessAttackerOnKillEffects(DamageReport report)
        {
            var selfBody = report.attackerBody;
            var ownerBody = report.attackerOwnerMaster.GetBody();

            if (!selfBody)
                return;

            if (!ownerBody || ownerBody == selfBody)
                return;

            if (Util.CheckRoll(ChanceToPassOnHit, ownerBody.master))
            {
                report.attacker = ownerBody.gameObject;
                report.attackerBody = ownerBody;
                report.attackerBodyIndex = BodyCatalog.FindBodyIndex(report.attackerBody);
                report.attackerMaster = ownerBody.master;
                report.attackerTeamIndex = ownerBody.master.teamIndex;
                report.damageInfo.attacker = ownerBody.gameObject;
                report.damageInfo.inflictor = ownerBody.gameObject;

                GlobalEventManager.instance.OnCharacterDeath(report);
            }
        }
    }
}
