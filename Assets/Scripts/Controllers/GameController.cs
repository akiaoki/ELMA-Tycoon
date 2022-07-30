using System;
using System.Collections;
using System.Collections.Generic;
using ELMA.SDK.Models;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {

        public UserModel UserModel { get; private set; }
        public UserOfficeModel UserOfficeModel { get; private set; }

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
        }

        public void Start()
        {
            LoadDefaultUser();
        }

        public void LoadDefaultUser()
        {
            UserModel = new UserModel
            {
                CompanyName = "ELMA",
                Level = 1,
                Money = 100.0f,
                Nickname = "Player"
            };
            UserOfficeModel = new UserOfficeModel()
            {
                PurchasedItems = new List<PurchaseItemModel>()
            };
        }
    }
}