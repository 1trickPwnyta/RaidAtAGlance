using RimWorld;

namespace RaidAtAGlance
{
    [DefOf]
    public static class RaidStrategyDefOf
    {
        static RaidStrategyDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(RaidStrategyDefOf));
        }

        public static RaidStrategyDef StageThenAttack;
        public static RaidStrategyDef ImmediateAttack;
        public static RaidStrategyDef ImmediateAttackSmart;
        public static RaidStrategyDef ImmediateAttackBreaching;
        public static RaidStrategyDef ImmediateAttackBreachingSmart;
        public static RaidStrategyDef ImmediateAttackSappers;
        public static RaidStrategyDef Siege;
    }
}
