using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeePurchaseAction : PurchaseItemAction
{

    public float incomeIncrease;

    public override string GetDescription()
    {
        return "Приносит +" + incomeIncrease + "$/с";
    }

    public override void OnActionUpdate(UpdateActionResult state)
    {
        state.IncomeIncrease += incomeIncrease;
    }
}
