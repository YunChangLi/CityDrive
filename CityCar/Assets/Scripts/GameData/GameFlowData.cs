﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏难度即线路
/// </summary>
public enum DriveLevel
{
   Level_1,
   Level_2,
   Level_3

}
/// <summary>
/// 游戏语言类别
/// </summary>
public enum Language
{
    Chinese,
    Taiwan,
    English
}
/// <summary>
/// 提醒方式
/// </summary>
public enum RemindType
{
    Text,
    Voice,
    Text_Voice
}

public enum DriveEnd
{
    Jiayouzhan,
    Youju
}

public enum MathDifficulty
{
    Easy,
    Normal,
    Hard
}

public class GameFlowData
{
    public DriveEnd DriveEnd { get; set; }

    public DriveLevel DriveLevel { get; set; }

    public RemindType RemindType { get; set; }

    public Language Language { get; set; }

    public string UserID { get; set; }

    public LineData.Data RouteNode { get; set; }

    public float Frequency { get; set; }

    public float TimeLimit { get; set; }

    public MathDifficulty Difficulty { get; set; }

    public GameFlowData(DriveEnd driveEnd, DriveLevel driveLevel, RemindType remindType, Language language, string ID, LineData.Data data, float frequency, float timeLimit, MathDifficulty difficulty)
    {
        DriveLevel = driveLevel;
        DriveEnd = driveEnd;
        RemindType = remindType;
        Language = language;
        UserID = ID;
        RouteNode = data;
        Frequency = frequency;
        TimeLimit = timeLimit;
        Difficulty = difficulty;
    }
}
