using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;
using System.Linq;
using System;
using I2.Loc;
using UnityEngine.XR;
using LabVisualization;


public class GameApplication : MonoSingleton<GameApplication>
{
    private List<IGameManager> _mangerList;
    public Language GameLanguage { get; set; }
    //private Language currentlanguage = Language.Chinese;


    private void Start()
    {
        //开始做什么
        GameApplicationInit();
        DontDestroyOnLoad(this);
        XRSettings.enabled=GameTaskManager.Instance.GameTaskConfig.IsOpenVR;
    }


    public void SetLanguage(string Language)
    {
        LocalizationManager.CurrentLanguage = Language;
        List<string> a = LocalizationManager.GetTermsList();
        FindObjectsOfType<MonoBehaviour>().OfType<ILanguageTranslate>().ToList<ILanguageTranslate>().ForEach(p => p.LanguageTraslate());
    }

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void GameApplicationInit()
    {
        LocalizationManager.InitializeIfNeeded();
        _mangerList = FindObjectsOfType<MonoBehaviour>().OfType<IGameManager>().ToList();
        _mangerList.ForEach(p => p.ManagerInit());
        SetLanguage(GameTaskManager.Instance.GameTaskConfig.Language);
        //change scene在scenenmanager init做

    }

    public void ManagersInit(GameFlowData data)
    {
        _mangerList.ForEach(p => p.FlowData = data);
        StartGameFlow();
    }

    public void StartGameFlow()
    {
        GameSceneManager.Instance.ChangeScene(new List<Action>()   
        {
            //VisualizationManager.Instance.VisulizationInit,
            //VisualizationManager.Instance.StartDataVisualization,
            GameDataManager.Instance.LabDataInit,
            GamePlayerManager.Instance.PlayerInit,
            GameUIManager.Instance.MainSceneUiInit,
            GameTaskManager.Instance.StartTaskMode,

        });
    }

    public void ManagersDispose()
    {
        _mangerList.ForEach(p => p.FlowData = null);
        _mangerList.ForEach(p => p.ManagerDispose());
    }


}
