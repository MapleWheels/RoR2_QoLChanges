using RoR2QoLChanges.Configuration;
using MiniRpcLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Additions.Networking
{
    public class MiniRpcManager
    {
        private MiniRpcInstance _miniRpcInstance;
        public MiniRpcInstance MiniRpcInstance
        {
            get => _instance._miniRpcInstance;
        }

        private static MiniRpcManager _instance;
        public static MiniRpcManager Instance
        {
            get
            {
                if (_instance == null)
                    Init();
                return _instance;
            }
        }

        private static void Init()
        {
            
            _instance = new MiniRpcManager();
            _instance._miniRpcInstance = MiniRpcLib.MiniRpc.CreateInstance(ConVars.PackageName);
        }
    }
}
