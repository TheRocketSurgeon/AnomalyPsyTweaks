using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace AnomalyPsyTweaks
{
    [HarmonyPatch]
    [HarmonyPatch(typeof(CompGoldenCube), "GiveHediff")]
    class Patch_CubeCompEffects
    {
        public static bool Prefix(CompGoldenCube __instance, ref Pawn pawn)
        {
            if (!AnomalyPsyTweaksSettings.TweakCube) return true; // skip prefix execution if tweak is disabled
            //Log.Message("APT-CCE: Attempting to add cube interest hediff to pawn " + pawn.Named("PAWN"));
            if (pawn.GetStatValue(StatDefOf.PsychicSensitivity) <= 0 && AnomalyPsyTweaksSettings.DoResistEffects)
            {
                //Log.Message("APT-CCE: Cube blocked from enthralling " + pawn.Named("PAWN") + " because they are not psychically sensitive");
                Log.Warning("APT-CCE: Attempting to select backup cube target");
                List<Pawn> freeColonistsSpawned = __instance.parent.Map.mapPawns.FreeColonistsSpawned;
                foreach(Pawn colonist in freeColonistsSpawned)
                {
                    if (0 < colonist.GetStatValue(StatDefOf.PsychicSensitivity))
                    {
                        if (!colonist.health.hediffSet.HasHediff(HediffDefOf.CubeInterest) && !colonist.health.hediffSet.HasHediff(HediffDefOf.CubeComa))
                        {
                            Log.Warning("APT-CCE: " + colonist.Named("PAWN") + " selected as backup. Adding cube interest hediff");
                            colonist.health.AddHediff(HediffDefOf.CubeInterest);
                            return false;
                        }
                    }
                }
                return false;
            }
            else
            {
                //Log.Message("APT-CCE: Continuing with standard execution");
                return true;
            }

        }
    }
}
