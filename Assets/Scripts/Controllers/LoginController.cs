using System;
using System.Collections;
using System.Collections.Generic;
using ELMA.SDK.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LoginController : MonoBehaviour
    {

        public GameObject loginMenu;
        public InputField loginField;

        private ELMAController _elmaController;
        private GameController _gameController;

        private void Awake()
        {
            _elmaController = FindObjectOfType<ELMAController>();
            _gameController = FindObjectOfType<GameController>();
        }

        public void Login()
        {
            loginMenu.SetActive(false);

            StartCoroutine(_elmaController.UserExists(loginField.text, result =>
            {
                if (!result)
                {
                    var user = new UserModel
                    {
                        CompanyName = "ELMA",
                        Level = 1,
                        Money = 100.0f,
                        Nickname = loginField.text
                    };
                    _gameController.UserModel = user;
                    StartCoroutine(_elmaController.CreateUser(user, creationResult =>
                    {
                        Debug.Log("User created");
                    }));
                }
                else
                {
                    Debug.Log("User exists");
                }
            }));
        }

    }
}