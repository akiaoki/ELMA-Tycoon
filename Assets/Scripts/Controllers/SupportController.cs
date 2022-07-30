using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Controllers
{
    public class SupportController : MonoBehaviour
    {

        public GameObject uiMenu;
        public UITicketLink uiTicketPrefab;
        
        private BuildController _buildController;
        private GameController _gameController;

        private void Awake()
        {
            _buildController = FindObjectOfType<BuildController>();
            _gameController = FindObjectOfType<GameController>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnTicket), 1.0f, 1.0f / BalanceController.GetTicketSpawnRate(_gameController.UserModel.Level));
        }

        public void ToggleMenu()
        {
            uiMenu.SetActive(!uiMenu.activeSelf);
        }

        private void SpawnTicket()
        {
            if (!uiMenu.activeSelf)
                return;
            
            var ticket = Instantiate(uiTicketPrefab.gameObject, uiTicketPrefab.transform.parent);
            var rt = (RectTransform)ticket.transform;
            var maxX = Screen.width / 2.5f - rt.sizeDelta.x / 2.0f;
            var x = UnityEngine.Random.Range(-maxX, maxX);
            rt.anchoredPosition = new Vector2(x, 500.0f);
            ticket.gameObject.SetActive(true);
        }
    }
}