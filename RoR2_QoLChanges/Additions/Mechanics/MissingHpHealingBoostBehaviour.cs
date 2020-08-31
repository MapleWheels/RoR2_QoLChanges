using RoR2;

using RoR2_QoLChanges.Additions.Buffs;
using RoR2_QoLChanges.Configuration.Buffs;

using RoR2QoLChanges;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2_QoLChanges.Additions.Mechanics
{
    /// <summary>
    /// This class is used to apply the boosted healing effect to buff to Captain's healing ward, and other effects.
    /// </summary>
    [RequireComponent(typeof(RoR2.HealingWard))]
    public class MissingHpHealingBoostBehaviour : MonoBehaviour
    {
		public static float
			healFraction = 0.02f,
			healPoints = 0f,
			interval =  0.2f,
			radius = 10f;
		public TeamFilter teamFilter;
		public static BuffIndex missingHpBuffIndex;
		public HealingWard AttachedWard;

        void Start()
        {
			missingHpBuffIndex = PluginCore.buffCatalog[nameof(MissingHpHealingBoostBuff)].BuffDef.buffIndex;
			AttachedWard = GetComponent<HealingWard>();

			if (AttachedWard)
            {
				teamFilter = AttachedWard.teamFilter;
				AttachedWard.healFraction = healFraction;
				AttachedWard.healPoints = healPoints;
				AttachedWard.interval = interval;
				AttachedWard.radius = radius;
			}
        }

        void Update()
        {
			if (missingHpBuffIndex == BuffIndex.None)
				return;

			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(this.teamFilter.teamIndex);
			float num = radius * radius;
			Vector3 position = base.transform.position;

			for (int i = 0; i < teamMembers.Count; i++)
			{
				CharacterBody component = teamMembers[i].GetComponent<CharacterBody>();
				if (component)
				{
					if ((teamMembers[i].transform.position - position).sqrMagnitude <= num)
					{
						if (!component.HasBuff(missingHpBuffIndex))
							component.AddBuff(missingHpBuffIndex);
					}
					else
					{
						if (component.HasBuff(missingHpBuffIndex))
							component.RemoveBuff(missingHpBuffIndex);
					}
				}
			}
		}
    }
}
