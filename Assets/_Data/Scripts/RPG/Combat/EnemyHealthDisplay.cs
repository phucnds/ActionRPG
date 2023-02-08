using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI EnemyHP;

        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
           if(fighter.GetTarget() == null)
           {
                EnemyHP.text = "Enemy: N/A";
                return;
           }
          
            EnemyHP.text = String.Format("Enemy: {0:0}%", fighter.GetTarget().GetPercentage());
           
        }
    }

}
