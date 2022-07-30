using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ELMA.SDK.Models
{
    public class UserModel : ElmaModel
    {
        [JsonProperty("idElma")]
        public string IdElma;
        [JsonProperty("nickName")]
        public string Nickname;
        [JsonProperty("companyName")]
        public string CompanyName;
        [JsonProperty("money")]
        public float Money;
        [JsonProperty("level")]
        public int Level;
        [JsonProperty("ofice")]
        public UserOfficeModel Office;
    }
}
