using R2API.Networking.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.Networking;

namespace RoR2QoLChanges.Additions.Networking
{
    /// <summary>
    /// Wrapper interface for provider agnostic implementation
    /// </summary>
    public interface INetHandle : INetMessage
    {
        new void OnReceived();
        new void Deserialize(NetworkReader reader);
        new void Serialize(NetworkWriter writer);
    }
}
