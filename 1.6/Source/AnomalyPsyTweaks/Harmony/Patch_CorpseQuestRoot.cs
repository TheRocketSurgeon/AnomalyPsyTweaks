using HarmonyLib;
using RimWorld;
using Verse;
using RimWorld.QuestGen;

namespace AnomalyPsyTweaks
{
    [HarmonyPatch]
    [HarmonyPatch(typeof(QuestNode_Root_MysteriousCargoUnnaturalCorpse), "ValidatePawn")]
    class Patch_CorpseQuestRoot
    {
        /// <summary>
        /// Blocks the unnatural corpse from targeting psy deaf pawns. If all normal targets are psy deaf event is skipped
        /// </summary>
        /// <param name="__result">result of standard QuestNode_Root_MysteriousCargoUnnaturalCorpse.ValidatePawn method</param>
        /// <param name="pawn">targeted pawn</param>
        /// <returns></returns>
        public static bool Postfix(bool __result, ref Pawn pawn)
        {
            //Log.Message("APT - CoQR: Corpse attempted to target a pawn with sensitivity of " + pawn.GetStatValue(StatDefOf.PsychicSensitivity));
            if (!AnomalyPsyTweaksSettings.TweakCorpse || AnomalyPsyTweaksSettings.DoCorpseHeadCrush)
            {
                //Log.Message("APT CorpseQuestRoot: Aborting postfix with result: " + __result);
                return __result; // skips postfix execution entirely when settings disable tweak or enable head crushing
            }
            if (__result && pawn.GetStatValue(StatDefOf.PsychicSensitivity) <= 0 && AnomalyPsyTweaksSettings.DoResistEffects)
            {
                //Log.Message("APT - CoQR: Marking " + pawn.Named("PAWN") + " as invalid unnatural corpse target");
                return false;
            }
            else
            {
                return __result;
            }

        }
    }
}
