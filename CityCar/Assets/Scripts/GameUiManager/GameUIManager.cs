using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;

public class GameUIManager : MonoSingleton<GameUIManager>, IGameManager
{
    public GameFlowData FlowData { get; set; }
    public MainUI MainUi { get; private set; }

    public VRSceneUI VRSceneUI { get; set; }

    public void MainSceneUiInit()
    {
        VRSceneUI = GameObject.FindObjectOfType<VRSceneUI>();
        
    }

    public void UiShowText()
    {
        VRSceneUI.SceneUiInit(/*FlowData.DriveEnd,FlowData.DriveLevel, */FlowData.RemindType);
    }

    public void ManagerDispose()
    {
        VRSceneUI = null;
        MainUi = null;
    }
    public void StartMainUiLogic()
    {
        MainUi = GameObject.FindObjectOfType<MainUI>();
        MainUi.MainUiInit();
    }

    public void ManagerInit()
    {
    }
}