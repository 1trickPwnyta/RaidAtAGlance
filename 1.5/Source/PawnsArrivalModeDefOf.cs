using RimWorld;

namespace RaidAtAGlance
{
    [DefOf]
    public static class PawnsArrivalModeDefOf
    {
        static PawnsArrivalModeDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnsArrivalModeDefOf));
        }

        public static PawnsArrivalModeDef EdgeWalkIn;
        public static PawnsArrivalModeDef EdgeDrop;
        public static PawnsArrivalModeDef EdgeWalkInGroups;
        public static PawnsArrivalModeDef EdgeDropGroups;
        public static PawnsArrivalModeDef CenterDrop;
        public static PawnsArrivalModeDef RandomDrop;
    }
}
