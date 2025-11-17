using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace CompletedQuestCleaner
{
    public class QuestCleanupMod : Mod
    {
        public class QuestCleanUpButton
        {
            public static void QuestCleanUp(Rect rect)
            {
                Rect questcleanuprect2 = rect;
                questcleanuprect2.x = rect.width - 135f;
                questcleanuprect2.y = rect.yMax - 24f - 10f;
                questcleanuprect2.width = 120f;
                questcleanuprect2.height = 24f;
                Text.Font = GameFont.Small;
                if (Widgets.ButtonText(questcleanuprect2, "BPHistoricalQuestCleanup".Translate()))
                {
                    var questManager = Find.QuestManager;
                    var questCleanup = new List<Quest>();
                    foreach (Quest quest in questManager.QuestsListForReading) // Error's if quests are removed on historical check.
                    {
                        if (ShouldCleanUp(quest)) 
                        { 
                            questCleanup.Add(quest); 
                        }
                    }
                    foreach (Quest QuestCleanup in questCleanup) 
                    {
                        questManager.Remove(QuestCleanup);
                    }
                    //questCleanup.Empty();
                }
            }
            public static bool ShouldCleanUp(Quest quest)
            {
                if (quest.root.defName == "RelicHunt") { return false; }
                return quest.Historical;
            }
        }

        public QuestCleanupMod(ModContentPack content) : base(content)
        {
            Harmony harmony = new Harmony("bluepentagram.questcleaner");
            MethodInfo original = AccessTools.Method(typeof(MainTabWindow_Quests), "DoWindowContents");
            HarmonyMethod postfix = new HarmonyMethod(typeof(QuestCleanUpButton).GetMethod("QuestCleanUp"));
            harmony.Patch(original, null, postfix);
            //Log.Message("Completed Quest Cleanup Patch Applied");
        }
    }
}