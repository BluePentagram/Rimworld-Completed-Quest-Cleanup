using RimWorld;
using UnityEngine;
using Verse;

namespace CompletedQuestCleaner
{
    public class Window_QuestUICleanUp : Window
    {
        QuestCleanupModSettings questSettings = LoadedModManager.GetMod<QuestCleanupModOptions>().GetSettings<QuestCleanupModSettings>();


        public override void PreOpen()
        {
            base.PreOpen();
            windowRect.height = 300f;
        }
        public override void DoWindowContents(Rect inRect)
        {
            Rect rect = new Rect(inRect.x, inRect.y, inRect.width, 32f);
            Text.Font = GameFont.Medium;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect, "BP.QuestCleanupButton".Translate());

            Text.Font = GameFont.Small;
            Text.Anchor = TextAnchor.UpperLeft;
            Widgets.DrawLine(new Vector2(inRect.x + 15f, rect.yMax + 5f), new Vector2(inRect.width - 15f, rect.yMax + 5f), GUI.color * new Color(1f, 1f, 1f, 0.4f), 1f);

            Rect autoQRect = new Rect(inRect.x, rect.yMax + 20f, inRect.width, 20f);
            Widgets.CheckboxLabeled(autoQRect, "BP.QuestAutoCleanup".Translate(), ref questSettings.questAutoCleanUp);

            Rect hideQRect = new Rect(inRect.x, autoQRect.yMax + 20f, inRect.width / 2, 80f);
                MakeQuestHidden(hideQRect);
            Rect removeQRect = new Rect(hideQRect.width, hideQRect.y, hideQRect.width, hideQRect.height);
                MakeQuestRemove(removeQRect);

            Text.Anchor = TextAnchor.MiddleCenter;
            GUI.color = Color.red;
            Rect test = new Rect(inRect.x, hideQRect.yMax, inRect.width, 60f);
            Widgets.Label(test, "BP.QuestUIRemoveWarning".Translate());
            Text.Anchor = TextAnchor.UpperLeft;
            GUI.color = Color.white;

            Rect ButtonLeft = new Rect(inRect.x, test.yMax, inRect.width / 3-5f, 32f);
            Rect ButtonMiddle = new Rect(ButtonLeft.width + 10f, ButtonLeft.y, ButtonLeft.width, ButtonLeft.height);
            Rect ButtonRight = new Rect(ButtonMiddle.x + ButtonMiddle.width + 10f, ButtonLeft.y, ButtonLeft.width, ButtonLeft.height);

            if (Widgets.ButtonText(ButtonLeft, "BP.QuestUIUnhide".Translate()))
            {
                QuestCleanUp.MassUnHide();
            }

            if (Widgets.ButtonText(ButtonMiddle, "BP.QuestUIClean".Translate()))
            {
                QuestCleanUp.MassClean();
            }

            if (Widgets.ButtonText(ButtonRight, "BP.QuestUIClose".Translate()))
            {
                this.Close();
            }
        }
        public override void PostClose()
        {
            questSettings.Write();
            base.PostClose();
        }
        private void MakeQuestHidden(Rect rect)
        {
            Listing_Standard listHideQuest = new Listing_Standard();
            listHideQuest.Begin(rect);
                listHideQuest.CheckboxLabeled("BP.QuestUIHideSuccess".Translate(), ref questSettings.HideSuccessQuest);
                listHideQuest.CheckboxLabeled("BP.QuestUIHideExpired".Translate(), ref questSettings.HideExpiredQuest);
                listHideQuest.CheckboxLabeled("BP.QuestUIHideFail".Translate(), ref questSettings.HideFailQuest);
            listHideQuest.End();
        }
        private void MakeQuestRemove(Rect rect)
        {
            Listing_Standard listRemoveQuest = new Listing_Standard();
            listRemoveQuest.Begin(rect);
                listRemoveQuest.CheckboxLabeled("BP.QuestUIRemoveSuccess".Translate(), ref questSettings.RemoveSuccessQuest, "BP.QuestUIRemoveWarning".Translate());
                listRemoveQuest.CheckboxLabeled("BP.QuestUIRemoveExpired".Translate(), ref questSettings.RemoveExpiredQuest);
                listRemoveQuest.CheckboxLabeled("BP.QuestUIRemoveFail".Translate(), ref questSettings.RemoveFailQuest);
            listRemoveQuest.End();
        }
    }
}
