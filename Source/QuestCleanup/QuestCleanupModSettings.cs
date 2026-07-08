using RimWorld;
using UnityEngine;
using Verse;

namespace CompletedQuestCleaner
{
    public class QuestCleanupModSettings : ModSettings
    {
        public bool questAutoCleanUp = false;

        public bool HideSuccessQuest = false;
        public bool HideFailQuest = false;
        public bool HideExpiredQuest = false;

        public bool RemoveSuccessQuest = false;
        public bool RemoveFailQuest = false;
        public bool RemoveExpiredQuest = false;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref questAutoCleanUp, "questAutoCleanUp");

            Scribe_Values.Look(ref HideSuccessQuest, "HideSuccessQuest");
            Scribe_Values.Look(ref HideFailQuest, "HideFailQuest");
            Scribe_Values.Look(ref HideExpiredQuest, "HideExpiredQuest");

            Scribe_Values.Look(ref RemoveSuccessQuest, "RemoveSuccessQuest");
            Scribe_Values.Look(ref RemoveFailQuest, "RemoveFailQuest");
            Scribe_Values.Look(ref RemoveExpiredQuest, "RemoveExpiredQuest");
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
            listingStandard.Label("BP.QuestCleanupSettingMessage".Translate());
            listingStandard.CheckboxLabeled("BP.QuestAutoCleanup".Translate(), ref settings.questAutoCleanUp);
            listingStandard.GapLine();
            listingStandard.CheckboxLabeled("BP.QuestUIHideSuccess".Translate(), ref settings.HideSuccessQuest);
            listingStandard.CheckboxLabeled("BP.QuestUIHideExpired".Translate(), ref settings.HideExpiredQuest);
            listingStandard.CheckboxLabeled("BP.QuestUIHideFail".Translate(), ref settings.HideFailQuest);
            listingStandard.GapLine();
            listingStandard.CheckboxLabeled("BP.QuestUIRemoveSuccess".Translate(), ref settings.RemoveSuccessQuest, "BP.QuestUIRemoveWarning".Translate());
            listingStandard.CheckboxLabeled("BP.QuestUIRemoveExpired".Translate(), ref settings.RemoveExpiredQuest);
            listingStandard.CheckboxLabeled("BP.QuestUIRemoveFail".Translate(), ref settings.RemoveFailQuest);
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "BP.QuestCleanupButton".Translate();
        }
    }
}
