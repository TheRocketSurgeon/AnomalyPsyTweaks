using HarmonyLib;
using RimWorld;
using Verse;

namespace AnomalyPsyTweaks
{
    [HarmonyPatch]
    [HarmonyPatch(typeof(CompGoldenCube), "ValidatePawn")]
    class Patch_CubeCompValidate
    {
        public static bool Postfix(bool __result, ref Pawn pawn)
        {
            if (!AnomalyPsyTweaksSettings.TweakCube) return __result; // skip postfix execution if tweak is disabled
            if (__result && pawn.GetStatValue(StatDefOf.PsychicSensitivity) <= 0 && AnomalyPsyTweaksSettings.DoResistEffects) // double checking result with updated criteria. should still honor default false results
            {
                return false;
            }
            // defaulting to default pick bool
            else
            {
                return __result;
            }
        }
    }
}