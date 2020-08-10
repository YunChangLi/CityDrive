using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;
[Serializable]
public class GameRouteConfig 
{
    public List<LineData.Data> RouteData { get; set; }

    public GameRouteConfig()
    {
    }
}
