using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLChanges.Additions.Networking
{
    public class NetworkingManager
    {
        public bool IsReady { get; private set; }

        private NetworkingManager _instance;
        protected INetRpcServiceProvider RpcServicesProvider;

        public virtual NetworkingManager Instance
        {
            get
            {
                if (this._instance == null)
                    this._instance = new NetworkingManager();
                return this._instance;
            }
            protected set
            {
                if (value != null)
                    this._instance = value;
            }
        }

        public void Initialize(INetRpcServiceProvider rpcProvider)
        {
            this.RpcServicesProvider = rpcProvider;
        }
    }
}
