using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace ELMA.SDK.Models
{
    public class PurchaseItemModel : ElmaModel
    {
        [JsonProperty("name")]
        public string Name;
        public Vector3 Location;
    }
}