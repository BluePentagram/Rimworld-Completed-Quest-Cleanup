using UnityEngine;
using Verse;

namespace CompletedQuestCleaner.QuestCleanup
{
    public class QuestCleanupModSettings : ModSettings
    {
        public bool questCleanUpMsgToLog = false;
        public bool questCleanUpButton = true;
        public override void ExposeData()
        {
            Scribe_Values.Look(ref questCleanUpMsgToLog, "questCleanUpMsgToLog");
            Scribe_Values.Look(ref questCleanUpButton, "questCleanUpButton");
            base.ExposeData();
        }
    }

    public class QuestCleanupMod : Mod
    {
        QuestCleanupModSettings settings;

        public QuestCleanupMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<QuestCleanupModSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("BPQuestCleanupLogging".Translate(), ref settings.questCleanUpMsgToLog, "BPQuestCleanupLoggingToolTip".Translate());
            listingStandard.Label("BPQuestCleanupRestartNeeded".Translate());
            listingStandard.CheckboxLabeled("BPQuestCleanupShowButton".Translate(), ref settings.questCleanUpButton, "BPQuestCleanupShowButtonToolTip".Translate());
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "BPQuestCleanupMod".Translate();
        }
    }
}
