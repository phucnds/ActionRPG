using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Inventories;
using System;

namespace RPG.Shops
{
    public class ShopItem
    {
        InventoryItem item;
        int avaliability;
        float price;
        int quantityInTransaction;

        public ShopItem(InventoryItem item, int avaliability, float price, int quantityInTransaction)
        {
            this.item = item;
            this.avaliability = avaliability;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;
        }

        public float GetPrice()
        {
            return price;
        }

        public int GetQuantity()
        {
            return quantityInTransaction;
        }

      

        public int GetAvailability()
        {
            return avaliability;
        }

        public string GetNameItem()
        {
            return item.GetDisplayName();
        }

        public Sprite GetIcon()
        {
            return item.GetIcon();
        }

        public InventoryItem GetInventoryItem()
        {
            return item;
        }
    }

}
