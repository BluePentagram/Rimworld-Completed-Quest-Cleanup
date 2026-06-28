using System.Collections.Generic;
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
            if (!LoadedModManager.GetMod<QuestCleanupModOptions>().GetSettings<QuestCleanupModSettings>().questUIDrawing)
            {
                return;
            }
            if (LoadedModManager.GetMod<QuestCleanupModOptions>().GetSettings<QuestCleanupModSettings>().questCleanUpButton)
            {
                Rect questcleanuprect2 = rect;
                questcleanuprect2.x = rect.width - 135f;
                questcleanuprect2.y = rect.yMax - 34f;
                questcleanuprect2.width = 120f;
                questcleanuprect2.height = 24f;
                Text.Font = GameFont.Small;
                if (Widgets.ButtonText(questcleanuprect2, "BPQuestCleanupButton".Translate()))
                {
                    var questManager = Find.QuestManager;
                    var questCleanup = new List<Quest>();
                    foreach (Quest quest in questManager.QuestsListForReading)
                    {
                        if (QuestCleanUp.ShouldCleanUp(quest))
                        {
                            questCleanup.Add(quest);
                        }
                    }
                    foreach (Quest QuestCleanup in questCleanup)
                    {
                        QuestCleanUp.QuestCleanupFunction(QuestCleanup);
                    }
                }
            }
            if (LoadedModManager.GetMod<QuestCleanupModOptions>().GetSettings<QuestCleanupModSettings>().questAutoCleanUp)
            {
                Rect questcleanuprect3 = rect;
                questcleanuprect3.x = rect.width * 0.36f + 24f;
                questcleanuprect3.y = rect.yMax - 24f;
                questcleanuprect3.width = rect.width;
                questcleanuprect3.height = 24f;
                Text.Font = GameFont.Small;
                Widgets.Label(questcleanuprect3, "BPAutoQuestCleanupUIText".Translate());
            }
        }
    }
}