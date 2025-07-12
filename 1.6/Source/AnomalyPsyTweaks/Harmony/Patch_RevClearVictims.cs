using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AnomalyPsyTweaks
{
    
    [HarmonyPatch]
    [HarmonyPatch(typeof(CompRevenant))]
    [HarmonyPatch(nameof(CompRevenant.PostDestroy))]
    class Patch_RevClearVictims
    {
        // does not check settings so APT revenant hediffs are still cleaned up if settings get changed mid event
        public static bool Prefix(CompRevenant __instance)
        {
            Log.Message("APT: clearing revenant victims");
            if (__instance.revenantVictims.NullOrEmpty())
            {
                return true;
            }
            foreach (Pawn revenantVictim in __instance.revenantVictims)
            {
                Log.Message("APT: checking " + revenantVictim.Named("NAME"));
                Hediff APT_hediffFailedHypno = revenantVictim?.health?.hediffSet?.GetFirstHediffOfDef(APT_DefOf.APT_FailedRevenantHypnosis);
                if (APT_hediffFailedHypno != null)
                {
                    Log.Message(revenantVictim.Named("NAME") + " has failed hypnosis. attempting removal");
                    revenantVictim.health.RemoveHediff(APT_hediffFailedHypno);
                }
                Hediff APT_hediffMildHypno = revenantVictim?.health?.hediffSet?.GetFirstHediffOfDef(APT_DefOf.APT_MildRevenantHypnosis);
                if (APT_hediffMildHypno != null)
                {
                    Log.Message(revenantVictim.Named("NAME") + " has partial hypnosis. attempting removal");
                    revenantVictim.health.RemoveHediff(APT_hediffMildHypno);
                }
                Hediff APT_hediffSevereHypno = revenantVictim?.health?.hediffSet?.GetFirstHediffOfDef(APT_DefOf.APT_SevereRevenantHypnosis);
                if (APT_hediffSevereHypno != null)
                {
                    Log.Message(revenantVictim.Named("NAME") + " has severe hypnosis. attempting removal");
                    revenantVictim.health.AddHediff(APT_DefOf.APT_HypnoticShock);
                    revenantVictim.health.RemoveHediff(APT_hediffSevereHypno);
                }
                Hediff APT_hediffExtremeHypno = revenantVictim?.health?.hediffSet?.GetFirstHediffOfDef(APT_DefOf.APT_ExtremeRevenantHypnosis);
                if (APT_hediffExtremeHypno != null)
                {
                    Log.Message(revenantVictim.Named("NAME") + " has extreme hypnosis. attempting removal");
                    revenantVictim.health.AddHediff(APT_DefOf.APT_ExtremeHypnoticShock);
                    revenantVictim.health.RemoveHediff(APT_hediffExtremeHypno);
                }
            }
            return true;
        }
    }
}