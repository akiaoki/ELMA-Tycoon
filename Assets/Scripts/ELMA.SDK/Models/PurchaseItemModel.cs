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

        [JsonIgnore]
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

        [JsonProperty("x")]
        public float X;
        [JsonProperty("y")]
        public float Y;
        [JsonProperty("z")]
        public float Z;
        
    }
}