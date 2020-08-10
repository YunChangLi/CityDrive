using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;


[Serializable]
public class GameTaskConfig
{
    public float Speed { get; set; }

    public int DriveType { get; set; }

    public int RedGreenLightTimer { get; set; }

    public bool IsOpenVR { get; set; }

    public string Language { get; set; }

    public GameTaskConfig()
    {
        Speed = 15;
        DriveType = 0;
        IsOpenVR = false;
        RedGreenLightTimer = 15;
        Language = "Taiwan";
    }
}
