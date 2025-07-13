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
            float severityOffset;
            if (!AnomalyPsyTweaksSettings.TweakCube) return; // skip postfix execution if tweak is disabled
            // dull pawns lose severity after initial severity increase to model resistance, but cannot go below min severity
            if (__instance.pawn.GetStatValue(StatDefOf.PsychicSensitivity) < 1 && AnomalyPsyTweaksSettings.DoResistEffects)
            {
                severityOffset = APT_SeverityPerPlay.RandomInRange * (1 - __instance.pawn.GetStatValue(StatDefOf.PsychicSensitivity));
                __instance.Severity -= severityOffset;
            }
            // sensitive pawns get a small severity increase after initial increase to model susceptibility
            else if (AnomalyPsyTweaksSettings.DoVulnEffects)
            {
                severityOffset = APT_SeverityPerPlay.RandomInRange * (__instance.pawn.GetStatValue(StatDefOf.PsychicSensitivity) - 1);
                __instance.Severity += severityOffset;
            }
        }
    }
}
