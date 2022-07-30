using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{

    public PurchaseItemDescription description;
    public List<PurchaseItemAction> actions;

    public string GetDescription()
    {
        foreach (var action in actions)
        {
            var v = action.GetDescription();
            if (!string.IsNullOrEmpty(v))
            {
                return v;
            }
        }

        return string.Empty;
    }

}
