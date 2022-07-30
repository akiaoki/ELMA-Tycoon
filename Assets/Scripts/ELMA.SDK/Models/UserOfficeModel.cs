using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ELMA.SDK.Models
{
    public class UserOfficeModel : ElmaModel
    {
        public string IdElma;
        public string UserNickname;
        public List<PurchaseItemModel> PurchasedItems;

    }
}