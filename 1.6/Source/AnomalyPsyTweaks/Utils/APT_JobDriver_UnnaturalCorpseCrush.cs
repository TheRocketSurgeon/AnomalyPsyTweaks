using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;
using Verse;

namespace AnomalyPsyTweaks
{
    public class APT_JobDriver_UnnaturalCorpseCrush : JobDriver
    {
        private const TargetIndex VictimIndex = TargetIndex.A;

        private Pawn Victim => base.TargetPawnA;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            //Log.Message("APT CorpseCrushDriver: initiating corpse crush job");
            this.FailOnDespawnedOrNull(TargetIndex.A);
            Find.Anomaly.TryGetUnnaturalCorpseTrackerForHaunted(Victim, out var tracker);
            int killTicks = (int)Math.Floor(tracker.TicksToKill * 1.2f);
            yield return Toils_Combat.FollowAndMeleeAttack(TargetIndex.A, TargetIndex.B, delegate
            {
                JobDriver curDriver = pawn.jobs.curDriver;
                int ticks = killTicks + 60;
                Find.Anomaly.Hypnotize(Victim, pawn, ticks);
                curDriver.ReadyForNextToil();
            });
            Toil toil = ToilMaker.MakeToil("MakeNewToils");
            toil.initAction = (Action)Delegate.Combine(toil.initAction, (Action)delegate
            {
                Messages.Message("MessageAwokenAttacking".Translate(Victim.Named("PAWN")), Victim, MessageTypeDefOf.NegativeEvent);
                tracker.Notify_AwokenAttackStarting();
            });
            toil.tickAction = (Action)Delegate.Combine(toil.tickAction, (Action)delegate
            {
                job.GetTarget(TargetIndex.A).Pawn.rotationTracker.FaceTarget(pawn);
                Victim.health.GetOrAddHediff(APT_DefOf.APT_AwokenGrab, null, null).TryGetComp<HediffComp_Disappears>().ticksToDisappear++;
                if (pawn.Drawer.renderer.CurAnimation != APT_DefOf.APT_UnnaturalCorpseAwokenKilling)
                {
                    pawn.Drawer.renderer.SetAnimation(APT_DefOf.APT_UnnaturalCorpseAwokenKilling);
                }
            });
            toil.defaultDuration = killTicks;
            toil.defaultCompleteMode = ToilCompleteMode.Delay;
            toil.PlaySustainerOrSound(SoundDefOf.Recipe_Surgery);
            toil.AddFinishAction(delegate
            {
                if (pawn.Drawer.renderer.CurAnimation == APT_DefOf.APT_UnnaturalCorpseAwokenKilling)
                {
                    pawn.Drawer.renderer.SetAnimation(null);
                }
            }
            );
            yield return toil;
            Toil toil2 = ToilMaker.MakeToil("MakeNewToils");
            toil2.initAction = delegate
            {
                Pawn pawn = job.GetTarget(TargetIndex.A).Pawn;
                BodyPartRecord head = pawn.health.hediffSet.GetBodyPartRecord(BodyPartDefOf.Head);
                if (head != null)
                {
                    Find.BattleLog.Add(new BattleLogEntry_Event(pawn, APT_DefOf.APT_Event_UnnaturalCorpseAttack, base.pawn));
                    DamageDef crush = APT_DefOf.APT_Crush;
                    BodyPartRecord hitPart = head;
                    pawn.TakeDamage(new DamageInfo(crush, 99999f, 99999f, -1f, base.pawn, hitPart));
                    Find.Anomaly.Notify_PawnKilledViaAwoken(pawn);
                }
            };
            toil2.PlaySustainerOrSound(SoundDefOf.CocoonDestroyed);
            yield return toil2;
        }

        public override bool IsContinuation(Job j)
        {
            return job.GetTarget(TargetIndex.A) == j.GetTarget(TargetIndex.A);
        }
    }
}
