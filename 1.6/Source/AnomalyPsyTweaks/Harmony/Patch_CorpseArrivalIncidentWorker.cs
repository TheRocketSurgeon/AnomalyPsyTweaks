using HarmonyLib;
using RimWorld;
using Verse;

namespace AnomalyPsyTweaks
{
    [HarmonyPatch(typeof(IncidentWorker_UnnaturalCorpseArrival), "ValidatePawn")]
    class Patch_CorpseArrivalIncidentWorker
    {
        /// <summary>
        /// Blocks the unnatural corpse from targeting psy deaf pawns. If all normal targets are psy deaf event is skipped
        /// </summary>
        /// <param name="__result">result of standard IncidentWorker_UnnaturalCorpseArrival.ValidatePawn method</param>
        /// <param name="pawn">targeted pawn</param>
        /// <returns></returns>
        public static bool Postfix(bool __result, ref Pawn pawn)
        {
            if (!AnomalyPsyTweaksSettings.TweakCorpse || AnomalyPsyTweaksSettings.DoCorpseHeadCrush)
            {
                //Log.Message("APT CorpseArrivalWorker: Aborting Postfix with result " + __result);
                return __result; // skips postfix execution entirely when settings disable tweak or enable head crushing
            }

            //Log.Message("APT - CoAIW: Corpse attempted to target a pawn with sensitivity of " + pawn.GetStatValue(StatDefOf.PsychicSensitivity));
            if (__result && pawn.GetStatValue(StatDefOf.PsychicSensitivity) <= 0 && AnomalyPsyTweaksSettings.DoResistEffects)
            {
                //Log.Message("APT - CoAIW: Marking " + pawn.Named("PAWN") + " as invalid unnatural corpse target");
                return false;
            }
            else
            {
                return __result;
            }
            
        }
    }
}
