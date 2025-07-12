using UnityEngine;
using Verse;

namespace AnomalyPsyTweaks
{
    class AnomalyPsyTweaksSettingsPage : Mod
    {
        /// A reference to our settings.
        AnomalyPsyTweaksSettings settings;

        /// A mandatory constructor which resolves the reference to our settings.
        public AnomalyPsyTweaksSettingsPage(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<AnomalyPsyTweaksSettings>();
        }

        /// The (optional) GUI part to set your settings.
        public override void DoSettingsWindowContents(Rect inRect)
        {
            //Rect lRect = new Rect(inRect.x, inRect.y, (inRect.width / 2f) - 15f, inRect.height);
            //Rect rRect = new Rect((inRect.x + inRect.width / 2f) + 15f, inRect.y, (inRect.width / 2f) - 15f, inRect.height);
            Listing_Standard listingStandard = new Listing_Standard();

            listingStandard.Begin(inRect);
            listingStandard.Label("APT_RestartNote".Translate());
            listingStandard.Label("APT_OngoingEventWarning".Translate());

            listingStandard.Gap();
            listingStandard.GapLine();
            listingStandard.Gap();

            listingStandard.Label("APT_EventSettingDesc".Translate());
            listingStandard.CheckboxLabeled("APT_RevTweak".Translate(), ref AnomalyPsyTweaksSettings.TweakRevenant, "APT_RevTweakDesc".Translate());
            listingStandard.CheckboxLabeled("APT_GoldCubeTweak".Translate(), ref AnomalyPsyTweaksSettings.TweakCube, "APT_GoldCubeTweakDesc".Translate());
            listingStandard.CheckboxLabeled("APT_CorpseTweak".Translate(), ref AnomalyPsyTweaksSettings.TweakCorpse, "APT_CorpseTweakDesc".Translate());
            listingStandard.CheckboxLabeled("APT_PainTweak".Translate(), ref AnomalyPsyTweaksSettings.TweakAgonyPulse, "APT_PainTweakDesc".Translate());

            listingStandard.Gap();
            listingStandard.GapLine();
            listingStandard.Gap();

            listingStandard.Label("APT_LevelNote".Translate());
            listingStandard.CheckboxLabeled("APT_EnableResist".Translate(), ref AnomalyPsyTweaksSettings.DoResistEffects, "APT_EnableResistDesc".Translate());
            listingStandard.CheckboxLabeled("APT_EnableVuln".Translate(), ref AnomalyPsyTweaksSettings.DoVulnEffects, "APT_EnableVulnDesc".Translate());

            listingStandard.Gap();
            listingStandard.GapLine();
            listingStandard.Gap();

            listingStandard.Label("APT_EventModifiersDesc".Translate());
            listingStandard.CheckboxLabeled("APT_EnableCorpseHeadCrush".Translate(), ref AnomalyPsyTweaksSettings.DoCorpseHeadCrush, "APT_EnableCorpseHeadCrushDesc".Translate());

            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        /// Override SettingsCategory to show up in the list of settings.
        /// Using .Translate() is optional, but does allow for localization.
        public override string SettingsCategory()
        {
            return "APT_ModName".Translate();
        }
    }
}