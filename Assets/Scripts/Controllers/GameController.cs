using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ELMA.SDK.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {

        public Text uiMoney;
        public Text uiIncome;
        public Text uiPeople;
        public Text uiVersion;
        
        public UserModel UserModel { get; private set; }
        public UserOfficeModel UserOfficeModel { get; private set; }

        public float PassiveIncome { get; private set; }
        public int PeopleCount { get; private set; }

        private List<PurchaseItem> _purchasedItems;

        private ELMAController _elmaController;
        private BalanceController _balanceController;
        private BuildController _buildController;
        
        private void Awake()
        {
            _elmaController = FindObjectOfType<ELMAController>();
            _balanceController = FindObjectOfType<BalanceController>();
            _buildController = FindObjectOfType<BuildController>();

            _purchasedItems = new List<PurchaseItem>();
            
            LoadDefaultUser();
        }

        public void Start()
        {
            
            
            InvokeRepeating(nameof(SecondUpdate), 0.0f, 1.0f);
        }

        private void FixedUpdate()
        {
            uiMoney.text = UserModel.Money + "$" ?? "-";
            uiIncome.text = PassiveIncome + "$/s";
            uiVersion.text = "ELMA v: " + UserModel.Level;

            var people = 0;
            if (UserOfficeModel != null)
            {
                foreach (var itemModel in UserOfficeModel.PurchasedItems)
                {
                    var item = _buildController.ExistingPurchaseItems[itemModel.Name];
                    if (item.description.type == PurchaseItemType.Employee)
                        people++;
                }
            }

            uiPeople.text = "Компания: " + people;
            PeopleCount = people;
            
            var nextLevel = BalanceController.GetLevelUpPeopleRequired(UserModel.Level);
            if (PeopleCount >= nextLevel)
            {
                LevelUp();
            }
        }

        private void SecondUpdate()
        {
            var state = new UpdateActionResult();
            
            if (UserOfficeModel != null)
            {
                foreach (var itemModel in UserOfficeModel.PurchasedItems)
                {
                    var item = _buildController.ExistingPurchaseItems[itemModel.Name];

                    foreach (var action in item.actions)
                    {
                        action.OnActionUpdate(state);
                    }
                }
            }
            
            UserModel.Money += state.IncomeIncrease;

            PassiveIncome = state.IncomeIncrease;
        }

        public void LoadDefaultUser()
        {
            UserModel = new UserModel
            {
                CompanyName = "ELMA",
                Level = 1,
                Money = 1000.0f,
                Nickname = "Player"
            };
            UserOfficeModel = new UserOfficeModel()
            {
                PurchasedItems = new List<PurchaseItemModel>()
            };
        }

        public void LevelUp()
        {
            UserModel.Level++;
        }
    }
}