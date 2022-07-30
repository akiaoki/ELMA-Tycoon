using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItemAction : MonoBehaviour
{

    public virtual string GetDescription()
    {
        return string.Empty;
    }

    public virtual void OnActionUpdate(UpdateActionResult state)
    {
        
    }
    
}
