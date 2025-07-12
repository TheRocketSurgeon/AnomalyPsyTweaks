using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace AnomalyPsyTweaks
{
    internal class AnomalyPsyTweaksSettings : ModSettings
    {
        // controls for individual event types
        public static bool TweakRevenant = true;
        public static bool TweakCube = true;
        public static bool TweakCorpse = true;
        public static bool TweakAgonyPulse = true;
        // controls for severity level tweaks for all enabled events
        public static bool DoResistEffects = true;
        public static bool DoVulnEffects = true;
        // controls for additional event modifiers
        public static bool DoCorpseHeadCrush = false;
        public static bool DoRevenantHarm = false;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref TweakRevenant, "TweakRevenant");
            Scribe_Values.Look(ref TweakCube, "TweakCube");
            Scribe_Values.Look(ref TweakCorpse, "TweakCorpse");
            Scribe_Values.Look(ref TweakAgonyPulse, "TweakAgonyPulse");

            Scribe_Values.Look(ref DoResistEffects, "DoResistEffects");
            Scribe_Values.Look(ref DoVulnEffects, "DoVulnEffects");

            Scribe_Values.Look(ref DoCorpseHeadCrush, "DoCorpseHeadCrush");
            Scribe_Values.Look(ref DoRevenantHarm, "DoRevenantHarm");
            base.ExposeData();
        }
    }
}
