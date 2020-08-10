using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;



public class GameTaskManager : MonoSingleton<GameTaskManager>, IGameManager
{


    public GameTaskConfig GameTaskConfig { get; set; }

    public GameFlowData FlowData { get ; set ; }

    public IPlayerStartTest _playerStart { get; set; }

    public IEnumerator _IEnumeratorMode { get; set; }

    //开始实验
    public void StartTaskMode()
    {
        _IEnumeratorMode = _playerStart.StartGameLogic(UesrInput);
        StartCoroutine(_IEnumeratorMode);
    }

    public void ManagerDispose()
    {
        FlowData = null;
        StopCoroutine(_IEnumeratorMode);
        _IEnumeratorMode = null;
    }

    public void ManagerInit()
    {
        GameTaskConfig= GameDataManager.Instance.GetConfig<GameTaskConfig>();
        _playerStart = gameObject.AddComponent<GameStartMode>();
        
    }

    public bool UesrInput()
    {
        if (GameTaskManager.Instance.GameTaskConfig.DriveType == 0)
        {
            return Input.GetKeyDown(KeyCode.A);
        }
        else if (GameTaskManager.Instance.GameTaskConfig.DriveType == 1)
        {
            return Input.GetKeyDown(KeyCode.JoystickButton5);
        }
        else if (GameTaskManager.Instance.GameTaskConfig.DriveType == 2)
        {
            return Input.GetKeyDown(KeyCode.JoystickButton0);
        }


        return false;

    }
}
