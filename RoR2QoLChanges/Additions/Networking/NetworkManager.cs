using RoR2;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.Networking;
using R2API.Networking;

namespace RoR2QoLChanges.Additions.Networking
{
    public static class NetworkManager
    {        
        public static void SendDamageToServer(DamageInfo damage, HurtBox target,
            bool callDamage, bool callHitEnemy, bool callHitWorld)
        {
            NetworkingHelpers.DealDamage(damage, target, callDamage, callHitEnemy, callHitWorld);
        }

        public static void SendTimedBuffToClient(CharacterBody body,
            BuffIndex buff, int stacks = 1, float duration = -1f)
        {
            NetworkingHelpers.ApplyBuff(body, buff, stacks, duration);
        }
    }

    public static class SynchronizationServices
    {
        public static void SyncObject(this INetHandle sender)
        {

        }
    }
}
