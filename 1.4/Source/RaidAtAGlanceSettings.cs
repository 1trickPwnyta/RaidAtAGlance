using UnityEngine;
using Verse;

namespace RaidAtAGlance
{
    public class RaidAtAGlanceSettings : ModSettings
    {
        public static int IconSize = 20;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.Label("RaidAtAGlance_IconSize".Translate());
            string buffer = IconSize.ToString();
            listingStandard.IntEntry(ref IconSize, ref buffer);
            IconSize = Mathf.Clamp(IconSize, 8, 32);

            listingStandard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref IconSize, "RaidAtAGlance_IconSize");
        }
    }
}
