using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Controllers
{
    public class BuildMenuController : MonoBehaviour
    {
        public GameObject uiEmployeeButton;
        public GameObject uiBuildMenu;
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

        public void ToggleBuildMode()
        {
            _buildController.ToggleBuildMode();
            uiEmployeeButton.SetActive(uiBuildMenu.activeSelf);
            uiBuildMenu.SetActive(_buildController.BuildMode);
        }

        private void LoadUiItems()
        {
            foreach (var item in _buildController.ExistingPurchaseItems.Values)
            {
                if (item.description.type != PurchaseItemType.Furniture)
                    continue;
                
                var uiItem = Instantiate(uiItemPrefab, uiItemPrefab.transform.parent);
                uiItem.title.text = item.description.visualName;
                uiItem.price.text = item.description.price + "$";
                uiItem.preview.sprite = item.description.preview;
                uiItem.button.onClick.AddListener(() => SelectItem(item.description));
                uiItem.gameObject.SetActive(true);
            }
        }

        private void SelectItem(PurchaseItemDescription item)
        {
            _buildController.selectedItem = item;
        }
    }
}