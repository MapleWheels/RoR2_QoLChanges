using HarmonyLib;
using RoR2QoLChanges.Patches.Bugfix;

namespace RoR2QoLChanges.Patches
{
    public class HarmonyPatchable : IPatchable
    {
        protected Harmony harmonyInstance { get; private set; }

        public HarmonyPatchable(Harmony instance)
        {
            harmonyInstance = instance;
        }

        public virtual void ApplyPatches() { }
        public virtual void RemovePatches() 
        {
            if (harmonyInstance == null)
                return;

            harmonyInstance.UnpatchAll();
        }
    }
}
