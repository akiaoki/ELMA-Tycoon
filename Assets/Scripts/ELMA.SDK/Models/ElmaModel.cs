using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElmaModel
{
    public string Uid;

    public ElmaModel()
    {
        Uid = Guid.NewGuid().ToString();
    }
}
