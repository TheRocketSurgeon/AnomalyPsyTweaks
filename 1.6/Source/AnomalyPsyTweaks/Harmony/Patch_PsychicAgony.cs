using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AnomalyPsyTweaks
{
    [HarmonyPatch]
    [HarmonyPatch(typeof(CreepJoinerWorker_PsychicAgony), "ApplyOrRefreshHediff")]
    class Patch_PsychicAgony
    {
        public static bool Prefix(ref Pawn pawn)
        {
            if (!AnomalyPsyTweaksSettings.TweakAgonyPulse) return true; // skip prefix execution if tweak is disabled
            IntRange DisappearSeconds = new IntRange(2000, 4000);
            if (pawn.GetStatValue(StatDefOf.PsychicSensitivity) <= 0 && AnomalyPsyTweaksSettings.DoResistEffects)
            {
                //Log.Message("APT: blocking agony pulse for " + pawn.Named("PAWN") + " with sensitivity of " + pawn.GetStatValue(StatDefOf.PsychicSensitivity));
                return false; // pawn is not psychically sensitive. Blocking hediff
            }
            else if (pawn.GetStatValue(StatDefOf.PsychicSensitivity) < 1 && AnomalyPsyTweaksSettings.DoResistEffects) // Psychically dull. Giving hediff of reduced severity
            {
                //Log.Message("APT: using mild agony pulse for " + pawn.Named("PAWN") + " with sensitivity of " + pawn.GetStatValue(StatDefOf.PsychicSensitivity));
                if (pawn.health.hediffSet.TryGetHediff(APT_DefOf.APT_MildAgonyPulse, out var hediff))
                {
                    hediff.Severity = 0f;
                }
                else
                {
                    hediff = pawn.health.AddHediff(APT_DefOf.APT_MildAgonyPulse, null, null);
                }
                HediffComp_Disappears hediffComp_Disappears = (hediff as HediffWithComps)?.GetComp<HediffComp_Disappears>();
                if (hediffComp_Disappears != null)
                {
                    hediffComp_Disappears.ticksToDisappear = DisappearSeconds.RandomInRange * 60;
                }
                return false;
            }
            else if (1.2 <= pawn.GetStatValue(StatDefOf.PsychicSensitivity) && pawn.GetStatValue(StatDefOf.PsychicSensitivity) < 1.399 && AnomalyPsyTweaksSettings.DoVulnEffects) // Psychically sensitive. Giving hediff of increased severity
            {
                //Log.Message("APT: using severe agony pulse for " + pawn.Named("PAWN") + " with sensitivity of " + pawn.GetStatValue(StatDefOf.PsychicSensitivity));
                if (pawn.health.hediffSet.TryGetHediff(APT_DefOf.APT_SevereAgonyPulse, out var hediff))
                {
                    hediff.Severity = 0f;
                }
                else
                {
                    hediff = pawn.health.AddHediff(APT_DefOf.APT_SevereAgonyPulse, null, null);
                }
                HediffComp_Disappears hediffComp_Disappears = (hediff as HediffWithComps)?.GetComp<HediffComp_Disappears>();
                if (hediffComp_Disappears != null)
                {
                    hediffComp_Disappears.ticksToDisappear = DisappearSeconds.RandomInRange * 60;
                }
                return false;
            }
            else if (1.399 <= pawn.GetStatValue(StatDefOf.PsychicSensitivity) && AnomalyPsyTweaksSettings.DoVulnEffects) // Psychically hypersensitive. Giving hediff of extreme severity
            {
                //Log.Message("APT: using extreme agony pulse for " + pawn.Named("PAWN") + " with sensitivity of " + pawn.GetStatValue(StatDefOf.PsychicSensitivity));
                if (pawn.health.hediffSet.TryGetHediff(APT_DefOf.APT_ExtremeAgonyPulse, out var hediff))
                {
                    hediff.Severity = 0f;
                }
                else
                {
                    hediff = pawn.health.AddHediff(APT_DefOf.APT_ExtremeAgonyPulse, null, null);
                }
                HediffComp_Disappears hediffComp_Disappears = (hediff as HediffWithComps)?.GetComp<HediffComp_Disappears>();
                if (hediffComp_Disappears != null)
                {
                    hediffComp_Disappears.ticksToDisappear = DisappearSeconds.RandomInRange * 60;
                }
                return false;
            }
            else
            {
                //Log.Message("APT: using default agony pulse for " + pawn.Named("PAWN") + " with sensitivity of " + pawn.GetStatValue(StatDefOf.PsychicSensitivity));
                return true;
            }
        }
    }
}
