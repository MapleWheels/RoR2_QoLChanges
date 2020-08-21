using HarmonyLib;

namespace RoR2QoLChanges.Patches.Items
{
    public class HarmonyPatchable
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
