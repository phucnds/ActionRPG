using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class ProgressBar : MonoBehaviour
    {
        public float value;
        public float value_max;

        public Image fill;


        void Update()
        {
            float ratio = value / Mathf.Max(value_max,0.01f);
            fill.fillAmount = ratio;
        }
    }
}

