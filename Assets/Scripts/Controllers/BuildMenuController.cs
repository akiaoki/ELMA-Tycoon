using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class BuildMenuController : MonoBehaviour
    {
        public UIItemLink uiItemPrefab;
        
        private BuildController _buildController;

        private void Awake()
        {
            _buildController = FindObjectOfType<BuildController>();
        }

        private void Start()
        {
            LoadUiItems();
        }

        private void LoadUiItems()
        {
            foreach (var item in _buildController.ExistingPurchaseItems)
            {
                var uiItem = Instantiate(uiItemPrefab, transform);
                uiItem.title.text = item.description.name;
                uiItem.price.text = item.description.price + "$";
                uiItem.preview.sprite = item.description.preview;
                uiItem.gameObject.SetActive(true);
            }
        }
    }
}