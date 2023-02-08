using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using UnityEngine;

namespace RPG.UI
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] QuestItemUI questItemPrefab;
        QuestList questList;

        private void Start()
        {
            questList = GameObject.FindWithTag("Player").GetComponent<QuestList>();
            questList.onUpdate += Redraw;
            Redraw();
        }

        private void Redraw()
        {
            ClearQuest();
            foreach (QuestStatus status in questList.GetStatuses())
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questItemPrefab, transform);
                uiInstance.Setup(status);
            }
        }

        private void ClearQuest()
        {
            foreach (Transform item in transform)
            {
                Destroy(item.gameObject);
            }
        }
    }

}