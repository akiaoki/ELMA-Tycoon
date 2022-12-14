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

        private float _spawnTimeout = 1.0f;
        
        private void Awake()
        {
            _buildController = FindObjectOfType<BuildController>();
            _gameController = FindObjectOfType<GameController>();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (_spawnTimeout - Time.deltaTime <= 0)
            {
                _spawnTimeout = 1.0f / BalanceController.GetTicketSpawnRate(_gameController.UserModel.Level);
                SpawnTicket();
                return;
            }

            _spawnTimeout -= Time.deltaTime;
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
            var maxX = 100.0f;
            var x = UnityEngine.Random.Range(-maxX, maxX);
            rt.anchoredPosition = new Vector2(x, 500.0f);
            ticket.gameObject.SetActive(true);
        }
    }
}