using System.Collections;
using System.Collections.Generic;
using System.IO;
using LabData;
using Newtonsoft.Json;
using UnityEngine;

public class GameConfigManager : MonoSingleton<GameConfigManager>,IGameManager
{
    public GameConfig Config { get; private set; }

    public GameFlowData FlowData { get; set; }

    public void ManagerInit()
    {
        Config = GameDataManager.Instance.GetConfig<GameConfig>();
    }

    public void ManagerDispose()
    {
       
    }
}
