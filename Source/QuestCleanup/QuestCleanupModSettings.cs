using UnityEngine;
using Verse;

namespace CompletedQuestCleaner
{
    public class QuestCleanupModSettings : ModSettings
    {
        public bool questCleanUpMsgToLog = false;
        public bool questCleanUpButton = true;
        public bool questAutoCleanUp = true;
        public override void ExposeData()
        {
            Scribe_Values.Look(ref questCleanUpMsgToLog, "questCleanUpMsgToLog");
            Scribe_Values.Look(ref questCleanUpButton, "questCleanUpButton");
            Scribe_Values.Look(ref questAutoCleanUp, "questAutoCleanUp");
            base.ExposeData();
        }
    }

    public class QuestCleanupModOptions : Mod
    {
        QuestCleanupModSettings settings;

        public QuestCleanupModOptions(ModContentPack content) : base(content)
        {
            settings = GetSettings<QuestCleanupModSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("BPQuestCleanupShowButton".Translate(), ref settings.questCleanUpButton, "BPQuestCleanupShowButtonToolTip".Translate());
            listingStandard.CheckboxLabeled("BPAutoQuestCleanup".Translate(), ref settings.questAutoCleanUp, "BPAutoQuestCleanupToolTip".Translate());
            listingStandard.CheckboxLabeled("BPQuestCleanupLogging".Translate(), ref settings.questCleanUpMsgToLog, "BPQuestCleanupLoggingToolTip".Translate());
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "BPQuestCleanupMod".Translate();
        }
    }
}
