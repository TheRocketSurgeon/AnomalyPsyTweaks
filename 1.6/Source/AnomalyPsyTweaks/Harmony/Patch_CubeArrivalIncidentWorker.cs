using HarmonyLib;
using RimWorld;
using Verse;

namespace AnomalyPsyTweaks
{
    [HarmonyPatch]
    [HarmonyPatch(typeof(IncidentWorker_GoldenCubeArrival), "ValidatePawn")]
    class Patch_CubeArrivalIncidentWorker
    {
        /// <summary>
        /// Blocks the cube from selecting a psy deaf pawn as initial target
        /// </summary>
        /// <param name="__result">initial result of IncidentWorker_GoldenCubeArrival.ValidatePawn method</param>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static bool Postfix(bool __result, ref Pawn pawn)
        {
            if (!AnomalyPsyTweaksSettings.TweakCube) return __result; // skips postfix execution entirely when settings disable tweak
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
