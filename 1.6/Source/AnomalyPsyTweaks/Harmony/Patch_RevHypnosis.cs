using UnityEngine;
using RimWorld;
using Verse;
using HarmonyLib;

namespace AnomalyPsyTweaks
{
    [HarmonyPatch]
    [HarmonyPatch(typeof(CompRevenant))]
    [HarmonyPatch(nameof(CompRevenant.Hypnotize))]
    class Patch_RevHypnosis
    {
        public static bool Prefix(CompRevenant __instance, ref Pawn victim){
            if (!AnomalyPsyTweaksSettings.TweakRevenant) return true; // skips prefix execution if revenant tweaks are turned off
            Pawn revenant = (Pawn)(__instance.parent);
            //Log.Message("APT: Revenant attempted to hypnotize a pawn with psychic sensitivity of: " + victim.GetStatValue(StatDefOf.PsychicSensitivity));
            if (!victim.Dead)
            {
                //Log.Message("Checking Revenant conditions");
                // pawn is not psychically sensitive
                if (victim.GetStatValue(StatDefOf.PsychicSensitivity) <= 0 && AnomalyPsyTweaksSettings.DoResistEffects)
                {
                    //Log.Message("APT: Revenant hypnosis failure");
                    victim.health.AddHediff(APT_DefOf.APT_FailedRevenantHypnosis, null, null);
                    RestUtility.WakeUp(victim);
                    __instance.revenantVictims.Add(victim);
                    revenant.Drawer.renderer.SetAnimation(AnimationDefOf.RevenantSpasm);
                    revenant.mindState.lastEngageTargetTick = Find.TickManager.TicksGame;
                    revenant.mindState.enemyTarget = null;
                    __instance.nextHypnosis = Find.TickManager.TicksGame + Mathf.FloorToInt(RevenantUtility.SearchForTargetCooldownRangeDays.RandomInRange * 60000f);
                    if (PawnUtility.ShouldSendNotificationAbout(victim))
                    {
                        Find.LetterStack.ReceiveLetter("APT_LetterLabelHypnosisFailed".Translate(victim.Named("PAWN")), "APT_LetterHypnosisFailed".Translate(victim.Named("PAWN")), LetterDefOf.NegativeEvent, victim);
                    }
                    return false;
                }
                // pawn is psychally dull
                else if (victim.GetStatValue(StatDefOf.PsychicSensitivity) < 1 && AnomalyPsyTweaksSettings.DoResistEffects)
                {
                    //Log.Message("APT: Revenant hypnosis partial success");
                    victim.health.AddHediff(APT_DefOf.APT_MildRevenantHypnosis, null, null);
                    RestUtility.WakeUp(victim);
                    __instance.revenantVictims.Add(victim);
                    revenant.Drawer.renderer.SetAnimation(AnimationDefOf.RevenantSpasm);
                    revenant.mindState.lastEngageTargetTick = Find.TickManager.TicksGame;
                    revenant.mindState.enemyTarget = null;
                    __instance.nextHypnosis = Find.TickManager.TicksGame + Mathf.FloorToInt(RevenantUtility.SearchForTargetCooldownRangeDays.RandomInRange * 60000f);
                    if (PawnUtility.ShouldSendNotificationAbout(victim))
                    {
                        Find.LetterStack.ReceiveLetter("APT_LetterLabelHypnosisMild".Translate(victim.Named("PAWN")), "APT_LetterHypnosisMild".Translate(victim.Named("PAWN")), LetterDefOf.NegativeEvent, victim);
                    }
                    return false;
                }

                // pawn is psychically sensitive
                else if (1.399 <= victim.GetStatValue(StatDefOf.PsychicSensitivity) && victim.GetStatValue(StatDefOf.PsychicSensitivity) < 1.799 && AnomalyPsyTweaksSettings.DoVulnEffects)
                {
                    //Log.Message("APT: Revenant hypnosis bonus success");
                    victim.health.AddHediff(APT_DefOf.APT_SevereRevenantHypnosis, null, null);
                    RestUtility.WakeUp(victim);
                    __instance.revenantVictims.Add(victim);
                    revenant.Drawer.renderer.SetAnimation(AnimationDefOf.RevenantSpasm);
                    revenant.mindState.lastEngageTargetTick = Find.TickManager.TicksGame;
                    revenant.mindState.enemyTarget = null;
                    __instance.nextHypnosis = Find.TickManager.TicksGame + Mathf.FloorToInt(RevenantUtility.SearchForTargetCooldownRangeDays.RandomInRange * 60000f);
                    if (PawnUtility.ShouldSendNotificationAbout(victim))
                    {
                        Find.LetterStack.ReceiveLetter("APT_LetterLabelHypnosisSevere".Translate(victim.Named("PAWN")), "APT_LetterHypnosisSevere".Translate(victim.Named("PAWN")), LetterDefOf.NegativeEvent, victim);
                    }
                    return false;
                }
                // pawn is psychically hypersensitive
                else if (1.799 <= victim.GetStatValue(StatDefOf.PsychicSensitivity) && AnomalyPsyTweaksSettings.DoVulnEffects)
                {
                    //Log.Message("APT: Revenant hypnosis extreme success");
                    victim.health.AddHediff(APT_DefOf.APT_ExtremeRevenantHypnosis, null, null);
                    RestUtility.WakeUp(victim);
                    __instance.revenantVictims.Add(victim);
                    revenant.Drawer.renderer.SetAnimation(AnimationDefOf.RevenantSpasm);
                    revenant.mindState.lastEngageTargetTick = Find.TickManager.TicksGame;
                    revenant.mindState.enemyTarget = null;
                    __instance.nextHypnosis = Find.TickManager.TicksGame + Mathf.FloorToInt(RevenantUtility.SearchForTargetCooldownRangeDays.RandomInRange * 60000f);
                    if (PawnUtility.ShouldSendNotificationAbout(victim))
                    {
                        Find.LetterStack.ReceiveLetter("APT_LetterLabelHypnosisExtreme".Translate(victim.Named("PAWN")), "APT_LetterHypnosisExtreme".Translate(victim.Named("PAWN")), LetterDefOf.NegativeEvent, victim);
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else { return true; }
        }
    }
}