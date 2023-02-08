using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI PlayerHP;
        [SerializeField] TextMeshProUGUI PlayerExp;

        Health health;

        private void Awake() 
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update() 
        {
            PlayerHP.text = String.Format("Health: {0:0}%",health.GetPercentage());
        }
    }

}

