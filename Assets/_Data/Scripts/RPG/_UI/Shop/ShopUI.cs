using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Shops;
using TMPro;
using UnityEngine.UI;
using System;

namespace RPG.UI.Shops
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI shopName;
        [SerializeField] TextMeshProUGUI total;
        [SerializeField] Transform listRoot;
        [SerializeField] RowUI rowPrefab;
        [SerializeField] Button exit;
        [SerializeField] Button switchBuying;
        [SerializeField] Button buttonBuy;

        [SerializeField] Button filterAll;
        [SerializeField] Button filterWeapon;
        [SerializeField] Button filterShield;
        [SerializeField] Button filterAccessory;
        [SerializeField] Button filterHelmet;
        [SerializeField] Button filterAbility;

        Shopper shopper = null;
        Shop currentShop = null;

        Color originalColor;

        void Start()
        {
            originalColor = total.color;
            shopper = GameObject.FindWithTag("Player").GetComponent<Shopper>();
            if(shopper == null) return;
            shopper.activeShopChange += ShopChanged;  
            exit.onClick.AddListener(Close);
            buttonBuy.onClick.AddListener(ConfirmTransaction);
            switchBuying.onClick.AddListener(SwitchMode);
            ShopChanged();
        }

        
        void ShopChanged()
        {
            if(currentShop != null)
            {
                currentShop.onChange -= RefreshUI;
            }
            currentShop = shopper.GetActiveShop();
            gameObject.SetActive(currentShop != null);

            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.SetShop(currentShop);
            }

            if(currentShop == null) return;
            currentShop.onChange += RefreshUI;
            shopName.text = currentShop.GetShopName();

            RefreshUI();
        }

        private void RefreshUI()
        {
            foreach (Transform child in listRoot)
            {
                Destroy(child.gameObject);
            }

            foreach (ShopItem item in currentShop.GetFilteredItem())
            {
                RowUI row = Instantiate<RowUI>(rowPrefab,listRoot);
                row.Setup(currentShop, item);
            }
            total.text = currentShop.GetTransactionTotal() + "";
            total.color = currentShop.HasSufficientFunds() ? originalColor : Color.red;
            buttonBuy.interactable = currentShop.CanTransaction();

            switchBuying.GetComponentInChildren<TextMeshProUGUI>().text = currentShop.IsBuyingMode() ? "Switch to Selling" : "Switch to Buying";
            buttonBuy.GetComponentInChildren<TextMeshProUGUI>().text = currentShop.IsBuyingMode() ? "Buy" : "Sell";

            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.RefreshUI();
            }

        }

        public void Close()
        {
            shopper.SetActiveShop(null);
        }

        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();
        }

        public void SwitchMode()
        {
            currentShop.SelectMode(!currentShop.IsBuyingMode());
        } 

    }

}
