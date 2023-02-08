using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Inventories;
using System;

namespace RPG.UI
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI balanceField;
        
        Purse playerPurse = null;

        void Start()
        {
            playerPurse = GameObject.FindWithTag("Player").GetComponent<Purse>();
            if(playerPurse != null)
            {
                playerPurse.onChange += RefreshUI;
            }
            

            RefreshUI();

        }

        // Update is called once per frame
        void RefreshUI()
        {
            balanceField.text = playerPurse.GetBalance() + "";
        }
    }
}

