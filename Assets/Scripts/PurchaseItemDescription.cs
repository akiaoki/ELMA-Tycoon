using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PurchaseItemType
{
    Furniture, Employee
}

[System.Serializable]
public class PurchaseItemDescription
{

    public string name;
    public string visualName;
    public PurchaseItemType type;
    public float price;
    public Sprite preview;

}
