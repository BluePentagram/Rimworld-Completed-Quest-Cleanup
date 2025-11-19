using HarmonyLib;
using RimWorld;
using Verse;

namespace CompletedQuestCleaner
{
    [HarmonyPatch(typeof(Quest), "CleanupQuestParts")]
    public static class QuestAutoCleanup
    {
        [HarmonyPostfix]
        public static void HarmonyQuestAutoCleanup(Quest __instance)
        {
            if (LoadedModManager.GetMod<QuestCleanupModOptions>().GetSettings<QuestCleanupModSettings>().questAutoCleanUp)
            {
                if (QuestCleanUp.ShouldCleanUp(__instance))
                {
                    Find.QuestManager.Remove(__instance);
                    QuestCleanUp.QuestCleanupToLog(__instance.name + " Quest has been auto cleaned up");
                }
            }
        }
    }
}