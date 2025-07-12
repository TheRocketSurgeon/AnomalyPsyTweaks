using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RimWorld;
using Verse;
using HarmonyLib;

namespace AnomalyPsyTweaks
{
    [DefOf]
    public static class APT_DefOf
    {
        // revenant hypnosis hediffs
        public static HediffDef APT_FailedRevenantHypnosis;
        public static HediffDef APT_MildRevenantHypnosis;
        public static HediffDef APT_SevereRevenantHypnosis;
        public static HediffDef APT_ExtremeRevenantHypnosis;
        public static HediffDef APT_HypnoticShock;
        public static HediffDef APT_ExtremeHypnoticShock;
        // revenant hypnosis thoughts
        public static ThoughtDef APT_FailedHypnosisThought;
        public static ThoughtDef APT_MildHypnosisThought;
        public static ThoughtDef APT_HypnoticShockThought;
        public static ThoughtDef APT_ExtremeHypnoticShockThought;

        // agony pulse hediffs
        public static HediffDef APT_MildAgonyPulse;
        public static HediffDef APT_SevereAgonyPulse;
        public static HediffDef APT_ExtremeAgonyPulse;

        //Unnatural Corpse defs
        public static JobDef APT_UnnaturalCorpseCrush;
        public static HediffDef APT_AwokenGrab;
        public static AnimationDef APT_UnnaturalCorpseAwokenKilling;
        public static RulePackDef APT_Event_UnnaturalCorpseAttack;
        public static DamageDef APT_Crush;
        static APT_DefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(APT_DefOf));
        }
    }
}
