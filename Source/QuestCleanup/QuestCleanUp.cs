using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CompletedQuestCleaner
{
    [StaticConstructorOnStartup]
    public class QuestCleanUp
    {
        public static List<string> AntiRelicProgress = new List<string> { "RelicHunt", "AncientComplex_Standard", "Hack_Spacedrone", "Hack_WorshippedTerminal" };

        static QuestCleanUp()
        {
            Harmony harmony = new Harmony("BP_QuestCleanUp");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public static bool ShouldCleanUp(Quest quest)
        {
            if (AntiRelicProgress.Contains(quest.root.defName)) { return false; }
            return quest.Historical;
        }
        public static void QuestCleanupFunction(Quest quest)
        {
            QuestCleanupModSettings questSettings = LoadedModManager.GetMod<QuestCleanupModOptions>().GetSettings<QuestCleanupModSettings>();
            if (Prefs.DevMode)
            {
                Log.Message("[UI Quest Cleanup]: " + quest.name + " has been cleaned/removed from quest log.");
            }
            switch (quest.State)
            {
                case QuestState.EndedSuccess:
                    if (questSettings.HideSuccessQuest)
                    {
                        quest.hiddenInUI = true;
                    }
                    if (questSettings.RemoveSuccessQuest)
                    {
                        Find.QuestManager.Remove(quest);
                    }
                    break;
                case QuestState.EndedFailed:
                    if (questSettings.HideFailQuest)
                    {
                        quest.hiddenInUI = true;
                    }
                    if (questSettings.RemoveFailQuest)
                    {
                        Find.QuestManager.Remove(quest);
                    }
                    break;
                case QuestState.EndedOfferExpired:
                    if (questSettings.HideExpiredQuest)
                    {
                        quest.hiddenInUI = true;
                    }
                    if (questSettings.RemoveExpiredQuest)
                    {
                        Find.QuestManager.Remove(quest);
                    }
                    break;
                default:
                    
                    break;
            }
        }
        public static void MassClean()
        {
            var questManager = Find.QuestManager;
            var questCleanup = new List<Quest>();
            foreach (Quest quest in questManager.QuestsListForReading)
            {
                if (ShouldCleanUp(quest))
                {
                    questCleanup.Add(quest);
                }
            }
            foreach (Quest QuestCleanup in questCleanup)
            {
                QuestCleanupFunction(QuestCleanup);
            }
        }
        public static void MassUnHide()
        {
            foreach (Quest quest in Find.QuestManager.QuestsListForReading)
            {
                if (quest.hiddenInUI && quest.Historical)
                {
                    quest.hiddenInUI = false;
                }
            }
        }
    }
}