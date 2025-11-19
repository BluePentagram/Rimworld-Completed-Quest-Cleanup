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

        public static bool ShouldCleanUp(Quest quest)
        {
            if (quest.root.defName == "RelicHunt") { return false; }
            return quest.Historical;
        }

        public static void QuestCleanupToLog(string String)
        {
            if (!LoadedModManager.GetMod<QuestCleanupModOptions>().GetSettings<QuestCleanupModSettings>().questCleanUpMsgToLog) { return; }
            Log.Message("[Completed Quest Cleanup]: " + String);
        }
    }
}