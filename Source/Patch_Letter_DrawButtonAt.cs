using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RaidAtAGlance
{
    [HarmonyPatch(typeof(Letter))]
    [HarmonyPatch(nameof(Letter.DrawButtonAt))]
    public static class Patch_Letter_DrawButtonAt
    {
        private const float ICON_SIZE = 20f;
        private const float ICON_SPACING = 2f;
        private const float LETTER_WIDTH = 38f;
        private const float LETTER_HEIGHT = 30f;

        public static void Postfix(Letter __instance, float topY)
        {
            bool historyHasRaidInfo = RaidAtAGlanceMod.raidInfoHistory.ContainsKey(__instance);

            if (Event.current.type == EventType.Repaint && (RaidAtAGlanceMod.raidInfo != null || historyHasRaidInfo))
            {
                RaidAtAGlanceMod.RaidInfo info = historyHasRaidInfo ? RaidAtAGlanceMod.raidInfoHistory[__instance] : RaidAtAGlanceMod.raidInfo;
                RaidAtAGlanceMod.raidInfoHistory[__instance] = info;
                RaidAtAGlanceMod.raidInfo = null;

                List<Texture2D> icons = new List<Texture2D>();

                if (info.parms.raidStrategy == RaidStrategyDefOf.StageThenAttack)
                {
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_Preparing"));
                }
                else if (info.parms.raidStrategy == RaidStrategyDefOf.ImmediateAttackSmart)
                {
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_Smart"));
                }
                else if (info.parms.raidStrategy == RaidStrategyDefOf.ImmediateAttackBreaching)
                {
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_Breaching"));
                } 
                else if (info.parms.raidStrategy == RaidStrategyDefOf.ImmediateAttackBreachingSmart)
                {
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_Breaching"));
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_Smart"));
                }
                else if (info.parms.raidStrategy == RaidStrategyDefOf.ImmediateAttackSappers)
                {
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_Sapping"));
                }
                else if (info.parms.raidStrategy == RaidStrategyDefOf.Siege)
                {
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_Sieging"));
                }

                if (info.parms.raidArrivalMode == PawnsArrivalModeDefOf.EdgeWalkInGroups)
                {
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_MultipleAngles"));
                }
                else if (info.parms.raidArrivalMode == PawnsArrivalModeDefOf.EdgeDropGroups)
                {
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_MultipleAngles"));
                }
                else if (info.parms.raidArrivalMode == PawnsArrivalModeDefOf.CenterDrop)
                {
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_DroppingOnYou"));
                }
                else if (info.parms.raidArrivalMode == PawnsArrivalModeDefOf.RandomDrop)
                {
                    icons.Add(ContentFinder<Texture2D>.Get("UI/RaidAtAGlance_DroppingRandomly"));
                }

                float rightX = (float)UI.screenWidth - 12f;
                float timeSinceArrival = Time.time - __instance.arrivalTime;
                if (timeSinceArrival < 1f)
                {
                    topY -= (1f - timeSinceArrival) * 200f;
                    GUI.color = new Color(1f, 1f, 1f, timeSinceArrival / 1f);
                }
                if (!Mouse.IsOver(new Rect(rightX - LETTER_WIDTH, topY, LETTER_WIDTH, LETTER_HEIGHT)) && __instance.def.bounce && timeSinceArrival > 15f && timeSinceArrival % 5f < 1f)
                {
                    float num3 = (float)UI.screenWidth * 0.06f;
                    float num4 = 2f * (timeSinceArrival % 1f) - 1f;
                    float num5 = num3 * (1f - num4 * num4);
                    rightX -= num5;
                }

                foreach (Texture2D icon in icons)
                {
                    GUI.DrawTexture(new Rect(rightX - ICON_SIZE, topY + LETTER_HEIGHT - ICON_SIZE / 2, ICON_SIZE, ICON_SIZE), icon);
                    rightX -= ICON_SIZE + ICON_SPACING;
                }

                GameFont previousFont = Text.Font;
                Text.Font = GameFont.Small;
                string numberOfRaiders = "x" + info.pawns.Count;
                Vector2 textSize = Text.CalcSize(numberOfRaiders);
                Widgets.Label(new Rect(rightX - textSize.x, topY + LETTER_HEIGHT - textSize.y / 2, textSize.x, textSize.y), numberOfRaiders);
                rightX -= textSize.x + ICON_SPACING;
                Text.Font = previousFont;

                GUI.color = info.parms.faction.Color.ToTransparent(timeSinceArrival / 1f);
                GUI.DrawTexture(new Rect(rightX - ICON_SIZE, topY + LETTER_HEIGHT - ICON_SIZE / 2, ICON_SIZE, ICON_SIZE), info.parms.faction.def.FactionIcon);
                rightX -= ICON_SIZE + ICON_SPACING;
            }
        }
    }
}
