using Verse;
using HarmonyLib;
using System.Reflection;

namespace AnomalyPsyTweaks
{
    [StaticConstructorOnStartup]
    internal static class Main
    {
        static Main()
        {
            var harmony = new Harmony("AnomalyPsyTweaks");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
