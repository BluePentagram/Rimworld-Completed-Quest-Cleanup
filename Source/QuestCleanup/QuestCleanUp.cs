using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CompletedQuestCleaner
{
    [StaticConstructorOnStartup]
    public static class QuestCleanUp
    {
        static QuestCleanUp()
        {
            Harmony harmony = new Harmony("BP_QuestCleanUp");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
        public static List<string> AntiRelicProgress = new List<string> { "RelicHunt", "AncientComplex_Standard", "Hack_Spacedrone", "Hack_WorshippedTerminal" };

        public static bool ShouldCleanUp(Quest quest)
        {
            //if (quest.root.defName == "RelicHunt") { return false; }
            if (AntiRelicProgress.Contains(quest.root.defName)) { return false; }
            return quest.Historical;
        }

        public static void QuestCleanupFunction(Quest quest)
        {
            quest.hiddenInUI = true; // Quest UI Historical use's this to 'delelete' quest from it's historical tab.
            //Find.QuestManager.Remove(quest); // Removing will remove progress of quest if still going on.
            if (Prefs.DevMode) {
                Log.Message("[UI Quest Cleanup]: " + quest.name + " has been removed from quest log.");
            }
        }
    }
}