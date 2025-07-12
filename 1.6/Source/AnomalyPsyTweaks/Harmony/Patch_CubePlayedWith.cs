using HarmonyLib;
using RimWorld;
using Verse;

namespace AnomalyPsyTweaks
{
    [HarmonyPatch]
    [HarmonyPatch(typeof(Hediff_CubeInterest), "Notify_PlayedWith")]
    class Patch_CubePlayedWith
    {
        private static readonly FloatRange APT_SeverityPerPlay = new FloatRange(0.045f, 0.105f);
        public static void Postfix(Hediff_CubeInterest __instance)
        {
            if (!AnomalyPsyTweaksSettings.TweakCube) return; // skip postfix execution if tweak is disabled
            float severityOffset = APT_SeverityPerPlay.RandomInRange;
            //Log.Message("APT: " + __instance.pawn.Named("PAWN") + "played with cube. Current severity: " + __instance.Severity);
            // dull pawns lose a small ammount of severity after initial severity increase to model resistance, but cannot go below min severity
            if (__instance.pawn.GetStatValue(StatDefOf.PsychicSensitivity) < 1 && AnomalyPsyTweaksSettings.DoResistEffects)
            {
                //Log.Message("APT: " + __instance.pawn.Named("PAWN") + " is psychically dull. Offsetting severity");
                if (__instance.sourceHediffDef.minSeverity < __instance.Severity - severityOffset * 0.2f)
                {
                    __instance.Severity -= severityOffset * 0.2f;
                    //Log.Message("APT: decreased severity by " + severityOffset * 0.2f);
                }
                else
                {
                    __instance.Severity = __instance.sourceHediffDef.minSeverity;
                    //Log.Warning("APT: Attempted to set cube interest below minimum severity. Set to minumum instead");
                }
            }
            // sensitive pawns get a small severity increase after initial increase to model susceptibility
            else if (1.2 <= __instance.pawn.GetStatValue(StatDefOf.PsychicSensitivity) && __instance.pawn.GetStatValue(StatDefOf.PsychicSensitivity) < 1.399 && AnomalyPsyTweaksSettings.DoVulnEffects)
            {
                //Log.Message("APT: " + __instance.pawn.Named("PAWN") + " is psychically sensitive. Increasing severity");
                __instance.Severity += severityOffset * 0.2f;
                Log.Message("APT: increased severity by " + severityOffset * 0.2f);
            }
            // hyper-sensitive pawns get a larger severity increase after initial increase to model increased susceptibility
            else if (1.399 <= __instance.pawn.GetStatValue(StatDefOf.PsychicSensitivity) && AnomalyPsyTweaksSettings.DoVulnEffects)
            {
                //Log.Message("APT: " + __instance.pawn.Named("PAWN") + " is psychically hyper-sensitive. Increasing severity");
                __instance.Severity += APT_SeverityPerPlay.RandomInRange * 0.4f;
                //Log.Message("APT: increased severity by " + severityOffset * 0.4f);
            }
            //Log.Message("APT: Severity is now" + __instance.Severity);
        }
    }
}
