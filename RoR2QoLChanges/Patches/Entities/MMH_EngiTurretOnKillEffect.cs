﻿using RoR2QoLChanges.Additions.Mechanics;
using RoR2QoLChanges.Configuration.Survivors;
using RoR2;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RoR2QoLChanges.Patches.Entities
{
    public class MMH_EngiTurretOnKillEffect : MonoModPatchable
    {
        public static EngineerConfig ActiveConfig;

        public MMH_EngiTurretOnKillEffect(EngineerConfig config) => ActiveConfig = config;

        public override void ApplyPatches()
        {
            GlobalEventManager.onCharacterDeathGlobal += OnCharacterDeathPostProcess;
        }

        private void OnCharacterDeathPostProcess(DamageReport damageReport)
        {
            //suicide
            if (!damageReport.attacker || !damageReport.damageInfo.inflictor || damageReport.attacker == damageReport.victim || damageReport.damageInfo.inflictor == damageReport.victim)
                return;

            MinionOnKillProcBehaviour component = damageReport.attacker.GetComponent<MinionOnKillProcBehaviour>();  

            if (component)
            {
                component.ProcessAttackerOnKillEffects(damageReport);
                component.ChanceToPassOnHit = ActiveConfig.EngiTurret_ChanceOnKillProcAppliedToEngi.Value;
            }
        }
    }
}
