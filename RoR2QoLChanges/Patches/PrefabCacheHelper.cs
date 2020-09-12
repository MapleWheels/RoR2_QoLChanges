using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RoR2QoLChanges.Patches
{
    public class PrefabCacheHelper
    {
        protected Dictionary<string, GameObject> prefabCache;

        private static PrefabCacheHelper _instance;
        public static PrefabCacheHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PrefabCacheHelper();
                return _instance;
            }
        }

        public bool TryGetPrefab(string fullPrefabPath, out GameObject prefab)
        {
            prefab = null;
            if (prefabCache.TryGetValue(fullPrefabPath, out prefab))
                return true;
            prefab = Resources.Load<GameObject>(fullPrefabPath);
            if (!prefab)
                return false;
            prefabCache.Add(fullPrefabPath, prefab);
            return true;
        }

        public PrefabCacheHelper()
        {
            prefabCache = new Dictionary<string, GameObject>();
        }
    }
}
