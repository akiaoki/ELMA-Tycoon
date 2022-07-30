using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace ELMA.SDK.Models
{
    public class UserOfficeModel : ElmaModel
    {
        //public string UserNickname;
        [JsonProperty("listPurchase")]
        public List<PurchaseItemModel> PurchasedItems;

    }
}