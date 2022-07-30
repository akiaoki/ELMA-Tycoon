using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ElmaModel
{

    [JsonProperty("idUnity")]
    public string Uid;

    public ElmaModel()
    {
        Uid = Guid.NewGuid().ToString();
    }
}
