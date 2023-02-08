using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Quests;
using TMPro;

namespace RPG.UI
{
    public class AchievementCounterUI : MonoBehaviour
    {

        AchievementCounter achievementCounter;

        void Start()
        {
            achievementCounter = GameObject.FindWithTag("Player").GetComponent<AchievementCounter>();
            achievementCounter.onCountChanged += UpdateUI;
            UpdateUI();
        }

        // Update is called once per frame
        void UpdateUI()
        {
            GetComponent<TextMeshProUGUI>().text = "Enemy - " + achievementCounter.GetCounterValue("Enemy");
        }
    }
}

