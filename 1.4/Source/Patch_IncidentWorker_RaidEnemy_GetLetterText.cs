using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace RaidAtAGlance
{
    [HarmonyPatch(typeof(IncidentWorker_RaidEnemy))]
    [HarmonyPatch("GetLetterText")]
    public static class Patch_IncidentWorker_RaidEnemy_GetLetterText
    {
        public static bool Prefix(IncidentParms parms, List<Pawn> pawns)
        {
            RaidAtAGlanceMod.raidInfo = new RaidAtAGlanceMod.RaidInfo();
            RaidAtAGlanceMod.raidInfo.parms = parms;
            RaidAtAGlanceMod.raidInfo.pawns = pawns;
            return true;
        }
    }
}
