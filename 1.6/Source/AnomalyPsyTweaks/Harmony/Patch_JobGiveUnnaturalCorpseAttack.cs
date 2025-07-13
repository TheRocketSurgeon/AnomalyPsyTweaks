using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace AnomalyPsyTweaks
{
    [HarmonyPatch]
    [HarmonyPatch(typeof(JobGiver_UnnaturalCorpseAttack), "TryGiveJob")]
    class Patch_JobGiveUnnaturalCorpseAttack
    {
        public static Job Postfix(Job __result, ref Pawn pawn)
        {
            if (!AnomalyPsyTweaksSettings.DoCorpseHeadCrush || !AnomalyPsyTweaksSettings.DoResistEffects) {
                //Log.Message("APT JobCorpseAttack: Aborting Postfix with result of " + __result);
                return __result;
            }
            else
            {
                if (pawn.Downed)
                {
                    return null;
                }
                if (!Find.Anomaly.TryGetUnnaturalCorpseTrackerForAwoken(pawn, out var tracker))
                {
                    return null;
                }
                Pawn haunted = tracker.Haunted;
                if (haunted.DestroyedOrNull())
                {
                    return null;
                }
                if (0 < haunted.GetStatValue(StatDefOf.PsychicSensitivity)) // fall back on psy attack is pawn is psychically sensitive
                {
                    //Log.Message("APT JobCorpseAttack: DoCorpseHeadCrush is enabled but target is psy sensitive. Executing default job result");
                    return __result;
                }
                if (!haunted.Spawned)
                {
                    if (haunted.SpawnedOrAnyParentSpawned && pawn.CanReach(haunted.SpawnedParentOrMe, PathEndMode.Touch, Danger.Deadly))
                    {
                        Job job = JobMaker.MakeJob(JobDefOf.AttackMelee, haunted.SpawnedParentOrMe);
                        job.expiryInterval = 30;
                        return job;
                    }
                    return null;
                }
                if (pawn.CanReach(haunted, PathEndMode.Touch, Danger.Deadly))
                {
                    //Log.Message("APT JobCorpseAttack: Making APT_UnnaturalCorpseCrush job");
                    Job job2 = JobMaker.MakeJob(APT_DefOf.APT_UnnaturalCorpseCrush, haunted);
                    job2.expiryInterval = 120;
                    job2.checkOverrideOnExpire = true;
                    return job2;
                }
                return null;
            }
        }
    }
}
