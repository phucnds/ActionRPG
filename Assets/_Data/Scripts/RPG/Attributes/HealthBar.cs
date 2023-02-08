using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.UI;
using UnityEngine;
using UnityEngine.UI;
using RPG.Utils;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Canvas rootCanvas;
    [SerializeField] Image progressBar;
    [SerializeField] Sprite hightSprite;
    [SerializeField] Sprite lowSprite;


    LazyValue<float> maxHP;
    private void Awake() {
        maxHP = new LazyValue<float>(health.MaxHP);
    }

    private void Start() {
        maxHP.ForceInit();
    }

    void Update()
    {
        //Mathf.Approximately
        float ratio =  health.CurrentHP() / maxHP.value;
        if (ratio == 1 || ratio == 0)
        {
            rootCanvas.enabled = false;
            return;
        }
        rootCanvas.enabled = true;
        progressBar.sprite = ratio <= 0.3 ? lowSprite : hightSprite;
        progressBar.transform.localScale = new Vector3(ratio,1,1);
    }
}
