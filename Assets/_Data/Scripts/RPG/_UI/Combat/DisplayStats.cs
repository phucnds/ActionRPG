using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using RPG.Stats;
using RPG.Combat;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class DisplayStats : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI PlayerHP;
        [SerializeField] TextMeshProUGUI PlayerMP;
        [SerializeField] TextMeshProUGUI PlayerLv;
      
        [SerializeField] TextMeshProUGUI PlayerName;

        [SerializeField] Slider progressPlayerHP;
        [SerializeField] Slider progressPlayerMP;
        [SerializeField] Slider progressPlayerXP;

        Health health;
        Mana mana;
        BaseStats baseStats;
        Fighter fighter;
        Experience exp;
        

        private void Awake()
        {
            GameObject player = GameObject.FindWithTag("Player");
            health = player.GetComponent<Health>();
            mana = player.GetComponent<Mana>();
            baseStats = player.GetComponent<BaseStats>();
            exp = player.GetComponent<Experience>();
            fighter = player.GetComponent<Fighter>();
            PlayerName.text = player.name;

            progressPlayerHP.interactable = false;
            progressPlayerXP.interactable = false;
            progressPlayerMP.interactable = false;

            progressPlayerXP.value = 0.2f;
        }

        private void FixedUpdate()
        {


            progressPlayerHP.value = health.CurrentHP() / Mathf.Max(health.MaxHP(), 0.01f);
            progressPlayerMP.value = mana.GetMana() / Mathf.Max(mana.GetMaxMana(), 0.01f);

            PlayerMP.text = mana.GetMagicPoint();
            PlayerHP.text = health.GetHealthPoint();
            PlayerLv.text = baseStats.GetLevel() + "";

            if(exp.GetPoints() == exp.GetExpToLvUp())
            {
                progressPlayerXP.value = 0/exp.GetExpToLvUp();
            }
            else
            {
                progressPlayerXP.value = exp.GetPoints() / exp.GetExpToLvUp();
                
            }   
        }
    }

}

