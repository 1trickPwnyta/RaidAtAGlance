namespace RaidAtAGlance
{
    public static class Debug
    {
        public static void Log(string message)
        {
#if DEBUG
            Verse.Log.Message($"[{RaidAtAGlanceMod.PACKAGE_NAME}] {message}");
#endif
        }
    }
}
