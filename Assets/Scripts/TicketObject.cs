using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

public class TicketObject : MonoBehaviour
{
    public enum TicketType
    {
        Fix, Develop, Cancel
    }

    public Sprite fixSprite;
    public Sprite developSprite;
    public Sprite cancelSprite;
    
    public float moveSpeed = 2.0f;
    public TicketType ticketType;

    private UITicketLink _link;
    private GameController _gameController;

    private bool _isHiding = false;

    private static Dictionary<TicketType, List<string>> _lines;

    private void Awake()
    {
        _link = GetComponent<UITicketLink>();
        _gameController = FindObjectOfType<GameController>();

        _lines = new Dictionary<TicketType, List<string>>();
        
        _lines.Add(TicketType.Fix, new List<string>()
        {
            "У клиента опять что-то сломалось!",
            "Пока клиент обновлял базу данных, у него вылезла ошибка",
            "Новое утро начинается с 404...",
            "Костыль упал кому-то на голову"
        });
        
        _lines.Add(TicketType.Develop, new List<string>()
        {
            "Нужно добавить категории объектов при поиске",
            "На форме создания объекта собака нужно добавить владельца",
            "Шрифты вышли погулять",
            "Дизайнер игрался со шрифтами и проиграл... Нужно править"
        });
        
        _lines.Add(TicketType.Cancel, new List<string>()
        {
            "Клиент хочет, чтобы система работала на спутнике",
            "Клиент хочет наши исходники",
            "\"Да я ничего не менял, это у вас сломалось!\"",
            "Клиент хочет большего!",
            "Клиент опять не хочет платить...",
            "Клиент ответил \"да\" на вопрос \"или\"..."
        });
    }

    private void Start()
    {
        ticketType = (TicketType)Random.Range(0, 3);
        moveSpeed = BalanceController.GetTicketMovement(_gameController.UserModel.Level);
        var lines = _lines[ticketType];
        _link.descriptionText.text = lines[Random.Range(0, lines.Count)];
        switch (ticketType)
        {
            case TicketType.Fix:
                _link.icon.sprite = fixSprite;
                break;
            case TicketType.Develop:
                _link.icon.sprite = developSprite;
                break;
            case TicketType.Cancel:
                _link.icon.sprite = cancelSprite;
                break;
        }
    }
    
    private void Update()
    {
        var rectTransform = (RectTransform)transform;

        rectTransform.anchoredPosition += Vector2.down * moveSpeed * Time.deltaTime;
        
        if (rectTransform.anchoredPosition.y < -1000.0f)
            Destroy(gameObject); // Делаем харакири

        if (_isHiding)
        {
            if (_link.canvasGroup.alpha - Time.deltaTime * 2.0f < 0)
            {
                Destroy(gameObject);
                return;
            }
            _link.canvasGroup.alpha -= Time.deltaTime * 2.0f;
        }
    }

    public void Fix()
    {
        if (_isHiding)
            return;

        if (ticketType == TicketType.Fix)
        {
            _gameController.UserModel.Money += BalanceController.GetTicketBonus(_gameController.UserModel.Level);
        }
        else
        {
            _link.background.color = new Color(1.0f, 0.7f, 0.7f, 1.0f);
        }
        
        _isHiding = true;
    }

    public void Develop()
    {
        if (_isHiding)
            return;
        
        if (ticketType == TicketType.Develop)
        {
            _gameController.UserModel.Money += BalanceController.GetTicketBonus(_gameController.UserModel.Level);
        }
        else
        {
            _link.background.color = new Color(1.0f, 0.7f, 0.7f, 1.0f);
        }
        
        _isHiding = true;
    }

    public void Cancel()
    {
        if (_isHiding)
            return;
        
        if (ticketType == TicketType.Cancel)
        {
            _gameController.UserModel.Money += BalanceController.GetTicketBonus(_gameController.UserModel.Level);
        }
        else
        {
            _link.background.color = new Color(1.0f, 0.7f, 0.7f, 1.0f);
        }
        
        _isHiding = true;
    }
}
