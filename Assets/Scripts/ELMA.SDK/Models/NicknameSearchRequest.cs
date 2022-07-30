using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ELMA.SDK.Models
{
    public class NicknameSearchRequest
    {
        [JsonProperty("nickName")]
        public string Nickname;
    }
}
