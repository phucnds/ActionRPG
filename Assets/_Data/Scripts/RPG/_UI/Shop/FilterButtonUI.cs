using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Inventories;
using RPG.Shops;
using System;

namespace RPG.UI
{
    public class FilterButtonUI : MonoBehaviour
    {
        [SerializeField] ItemCategory category = ItemCategory.None;
        [SerializeField] Image line;

        Button button;
        Shop currentShop;

        private void Awake() {
            button = GetComponent<Button>();
            button.onClick.AddListener(SelectFilter);
        }

        public void RefreshUI()
        {
            line.gameObject.SetActive(currentShop.GetFilter() == category);
        }

        public void SetShop(Shop currentShop)
        {
            this.currentShop = currentShop;
        }

        private void SelectFilter()
        {
            currentShop.SelectFilter(category);
        }
    }
}

