using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameConfig {

	public bool IsShowHuaShanLogo { get; set; }
    public bool IsOpenEyeTracing { get; set; }
    public string MainSceneName { get; set; }
    public bool EyeMovement { get; set; }
    public Language Language { get; set; }
    public string ServerPath { get; set; }

    public GameConfig()
    {
        IsShowHuaShanLogo = false;
        IsOpenEyeTracing = false;
        MainSceneName = "LoginUI";
        Language = Language.Chinese;
        ServerPath = "http://120.132.106.14:4000/api/data";
    }
}
