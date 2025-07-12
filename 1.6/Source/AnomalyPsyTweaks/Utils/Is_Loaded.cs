using RimWorld;
using Verse;

namespace TestMod
{
    [StaticConstructorOnStartup]
    public static class Is_Loaded
    {
        static Is_Loaded()
        {
            Log.Message("Anomaly Psy Tweaks loaded");
        }
    }
}