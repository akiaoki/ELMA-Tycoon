using System.Collections;
using System.Collections.Generic;
using ELMA.SDK.Models;
using UnityEngine;

namespace Controllers
{
    public class EmployeeController : MonoBehaviour
    {
        public GameObject uiBuildButton;
        public GameObject uiMenu;
        public UIItemLink uiItemPrefab;
        
        private BuildController _buildController;
        private GameController _gameController;
        
        private void Awake()
        {
            _buildController = FindObjectOfType<BuildController>();
            _gameController = FindObjectOfType<GameController>();
        }
        
        private void Start()
        {
            LoadUiItems();
        }

        public void ToggleMenu()
        {
            uiBuildButton.SetActive(uiMenu.activeSelf);
            uiMenu.SetActive(!uiMenu.activeSelf);
        }

        private void LoadUiItems()
        {
            foreach (var item in _buildController.ExistingPurchaseItems.Values)
            {
                if (item.description.type != PurchaseItemType.Employee)
                    continue;
                
                var uiItem = Instantiate(uiItemPrefab, uiItemPrefab.transform.parent);
                uiItem.title.text = item.description.visualName;
                uiItem.price.text = item.description.price + "$";
                uiItem.description.text = item.GetDescription();
                uiItem.button.onClick.AddListener(() => TryBuyItem(item.description));
                uiItem.gameObject.SetActive(true);
            }
        }
        
        private void TryBuyItem(PurchaseItemDescription item)
        {
            if (_gameController.UserModel.Money >= item.price)
            {
                _gameController.UserModel.Money -= item.price;
                var itemObj = Instantiate(_buildController.ExistingPurchaseItems[item.name].gameObject, transform);
                _gameController.UserOfficeModel.PurchasedItems.Add(new PurchaseItemModel { Name = item.name });
            }
            else
            {
                Debug.Log("Not enough money");
            }
        }
    }
}