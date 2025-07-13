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
            if (!AnomalyPsyTweaksSettings.TweakCorpse || AnomalyPsyTweaksSettings.DoCorpseHeadCrush)
            {
                return __result; // skips postfix execution entirely when settings disable tweak or enable head crushing
            }
            if (__result && pawn.GetStatValue(StatDefOf.PsychicSensitivity) <= 0 && AnomalyPsyTweaksSettings.DoResistEffects)
            {
                return false;
            }
            else
            {
                return __result;
            }

        }
    }
}
