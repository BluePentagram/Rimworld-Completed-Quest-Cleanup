using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;


namespace CompletedQuestCleaner
{
    [HarmonyPatch(typeof(MainTabWindow_Quests), "DoWindowContents")]
    public static class QuestCleanupUIWindow
    {
        [HarmonyPostfix]
        public static void HarmonyQuestCleanUpButton( Rect rect)
        {
            
            Rect questcleanuprect2 = rect;
            questcleanuprect2.x = rect.width - 145f;
            questcleanuprect2.y = rect.yMax - 34f;
            questcleanuprect2.width = 130f;
            questcleanuprect2.height = 24f;
            Text.Font = GameFont.Small;
            if (Widgets.ButtonText(questcleanuprect2, "BP.QuestCleanupButton".Translate()))
            {
                Find.WindowStack.Add(new Window_QuestUICleanUp());
            }
        }
    }
}