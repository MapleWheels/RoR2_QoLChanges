using RoR2;
using RoR2QoLChanges.Additions.Buffs;
using System.Collections.ObjectModel;
using UnityEngine;

namespace RoR2QoLChanges.Additions.Mechanics
{
    /// <summary>
    /// This class is used to apply the boosted healing effect to buff to Captain's healing ward, and other effects.
    /// </summary>
    [RequireComponent(typeof(RoR2.HealingWard))]
	[DisallowMultipleComponent]
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

		public void SyncHealingStats(bool isMaster = true)
        {
			if (AttachedWard)
			{
				if (isMaster)
				{
					AttachedWard.radius = radius;
					AttachedWard.healFraction = healFraction;
					AttachedWard.healPoints = healPoints;
					AttachedWard.radius = radius;
				}
                else
                {
					radius = AttachedWard.radius;
					healFraction = AttachedWard.healFraction;
					healPoints = AttachedWard.healPoints;
					radius = AttachedWard.radius;
				}
			}
        }

        void Update()
        {
			if (missingHpBuffIndex == BuffIndex.None)
				return;

			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamFilter.teamIndex);
			float sqrDist = radius * radius;
			Vector3 position = base.transform.position;

			for (int i = 0; i < teamMembers.Count; i++)
			{
				CharacterBody component = teamMembers[i].GetComponent<CharacterBody>();
				if (component)
				{
					if ((teamMembers[i].transform.position - position).sqrMagnitude <= sqrDist)
					{
						if (!component.HasBuff(missingHpBuffIndex))
							component.AddTimedBuff(missingHpBuffIndex, 0.25f);
					}
				}
			}
		}
    }
}
