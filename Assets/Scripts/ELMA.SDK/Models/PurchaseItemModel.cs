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

        public Vector3 Location
        {
            get => new Vector3(X, Y, Z);
            set
            {
                X = value.x;
                Y = value.y;
                Z = value.z;
            }
        }
        public float X, Y, Z;
        
    }
}