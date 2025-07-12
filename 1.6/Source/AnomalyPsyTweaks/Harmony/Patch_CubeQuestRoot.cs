using HarmonyLib;
using RimWorld;
using RimWorld.QuestGen;
using Verse;

namespace AnomalyPsyTweaks
{
    [HarmonyPatch]
    [HarmonyPatch(typeof(QuestNode_Root_MysteriousCargoUnnaturalCube), "ValidatePawn")]
    class Patch_CubeQuestRoot
    {
        public static bool Postfix(bool __result, ref Pawn pawn)
        {
            if (!AnomalyPsyTweaksSettings.TweakCube) return __result; // skip postfix execution if tweak is disabled
            if (__result && pawn.GetStatValue(StatDefOf.PsychicSensitivity) <= 0 && AnomalyPsyTweaksSettings.DoResistEffects) // double checking result with updated criteria. should still honor default false results
            {
                //Log.Message("APT-CQR: Cube blocked from selecting " + pawn.Named("PAWN") + " because they have psy sensitivity of zero");
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
