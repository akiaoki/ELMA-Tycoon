using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ELMA.SDK.Models
{
    public class NicknameSearchResponse
    {
        [JsonProperty("isFound")]
        public bool IsFound;
    }
}
