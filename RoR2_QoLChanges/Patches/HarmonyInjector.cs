using HarmonyLib;
using RoR2QoLChanges.Configuration;

namespace RoR2QoLChanges.Patches
{
    public static class HarmonyInjector
    {
        public static Harmony Instance { get; }

        static HarmonyInjector()
        {
            Instance = new Harmony(ConVars.PackageName);
        }
    }
}
