using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace CompletedQuestCleaner
{
    public class QuestCleanupModSettings : ModSettings
    {
        //public bool questCleanUpMsgToLog = false; // Removed Useing bas game Dev mode to check if should log to file.
        public bool questUIDrawing = true;
        public bool questCleanUpButton = true;
        public bool questAutoCleanUp = true;
        //public List<Quest> questNoCleanup = new List<Quest>(); // WIP Make a way to allow users to make a keep list of quest not just for relic hunts? maybe by XML list.

        public override void ExposeData()
        {
            //Scribe_Values.Look(ref questCleanUpMsgToLog, "questCleanUpMsgToLog");
            Scribe_Values.Look(ref questUIDrawing, "questUIDrawing");
            Scribe_Values.Look(ref questCleanUpButton, "questCleanUpButton");
            Scribe_Values.Look(ref questAutoCleanUp, "questAutoCleanUp");
            //Scribe_Values.Look(ref questNoCleanup, "questNoCleanup");
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
            listingStandard.CheckboxLabeled("BPQuestUIDrawing".Translate(), ref settings.questUIDrawing, "BPQuestUIDrawingTooltip".Translate());
            listingStandard.CheckboxLabeled("BPQuestCleanupShowButton".Translate(), ref settings.questCleanUpButton, "BPQuestCleanupShowButtonToolTip".Translate());
            listingStandard.CheckboxLabeled("BPQuestAutoCleanup".Translate(), ref settings.questAutoCleanUp, "BPQuestAutoCleanupToolTip".Translate());
            //listingStandard.CheckboxLabeled("BPQuestCleanupLogging".Translate(), ref settings.questCleanUpMsgToLog, "BPQuestCleanupLoggingToolTip".Translate());
            listingStandard.End();
            //Listing_TreeDefs listingTree = new Listing_TreeDefs(float.MaxValue);
            //listingTree.Begin(inRect);
            //listingTree.ContentLines(settings.questNoCleanup, 0);
            //listingTree.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "BPQuestCleanupMod".Translate();
        }
    }
}
