using Verse;
using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;

namespace RaidAtAGlance
{
    public class RaidAtAGlanceMod : Mod
    {
        public const string PACKAGE_ID = "raidataglance.1trickPonyta";
        public const string PACKAGE_NAME = "Raid at a Glance";

        public static RaidAtAGlanceSettings Settings;

        public class RaidInfo
        {
            public IncidentParms parms;
            public List<Pawn> pawns;
        }

        public static RaidInfo raidInfo;
        public static Dictionary<Letter, RaidInfo> raidInfoHistory = new Dictionary<Letter, RaidInfo>();

        public RaidAtAGlanceMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<RaidAtAGlanceSettings>();

            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            RaidAtAGlanceSettings.DoSettingsWindowContents(inRect);
        }
    }
}
