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

        private ELMAController _elmaController;
        private BalanceController _balanceController;
        private BuildController _buildController;
        
        private void Awake()
        {
            _elmaController = FindObjectOfType<ELMAController>();
            _balanceController = FindObjectOfType<BalanceController>();
            _buildController = FindObjectOfType<BuildController>();
        }

        public void Start()
        {
            
        }
    }
}