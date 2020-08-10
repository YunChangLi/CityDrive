using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;

public class GameNpcManager : MonoSingleton<GameNpcManager>,IGameManager
{
    GameFlowData IGameManager.FlowData { get ; set; }

    void IGameManager.ManagerDispose()
    {

    }

    void IGameManager.ManagerInit()
    {


    }

    
}
