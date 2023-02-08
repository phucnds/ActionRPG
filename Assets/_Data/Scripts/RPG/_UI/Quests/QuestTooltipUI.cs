using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using UnityEngine;
using TMPro;
using System;

namespace RPG.UI
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject objectivePrefab2;
        [SerializeField] TextMeshProUGUI reward;


        public void Setup(QuestStatus status)
        {
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();
            DetachChildren();
            foreach (var objective in quest.GetObjectives())
            {
                GameObject objectiveInstance = Instantiate(status.IsCompletedObjective(objective.reference) ?  objectivePrefab2 : objectivePrefab,objectiveContainer);
                objectiveInstance.GetComponentInChildren<TextMeshProUGUI>().text = objective.description;
            }
            reward.text = GetRewardText(quest);
        }

        private string GetRewardText(Quest quest)
        {
            string rewardText = "";

            foreach (var reward in quest.GetRewards())
            {
                if(rewardText != "")
                {
                    rewardText += ", ";
                }
                if(reward.number > 1)
                {
                    rewardText += reward.number + " "; 
                }
                rewardText += reward.item.GetDisplayName();
            }
            if(rewardText == "")
            {
                rewardText = "No reward.";
            }
            rewardText += ".";
            return rewardText;
        }

        private void DetachChildren()
        {
            foreach (Transform item in objectiveContainer)
            {
                Destroy(item.gameObject);
            }
        }

    }


    
}
