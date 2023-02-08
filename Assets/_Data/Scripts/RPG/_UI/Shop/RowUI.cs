using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Shops;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RowUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameItem;
    [SerializeField] TextMeshProUGUI avaliability;
    [SerializeField] TextMeshProUGUI price; 
    [SerializeField] TextMeshProUGUI quantity;

    [SerializeField] Button reduceButton;
    [SerializeField] Button increaseButton;

    Shop currentShop = null;
    ShopItem item = null;

    private void Start() {
        reduceButton.onClick.AddListener(Reduce);
        increaseButton.onClick.AddListener(Increase);
    }

    public void Setup(Shop currentShop, ShopItem item)
    {
        this.currentShop = currentShop;
        this.item = item;
        icon.sprite = item.GetIcon();
        nameItem.text = item.GetNameItem();
        avaliability.text = item.GetAvailability() + "";
        price.text = item.GetPrice() + "";
        quantity.text = item.GetQuantity() + "";
    }

    private void Increase()
    {
        currentShop.AddToTransaction(item.GetInventoryItem(),1);
    }

    private void Reduce()
    {
        currentShop.AddToTransaction(item.GetInventoryItem(), -1);
    }

    



}
