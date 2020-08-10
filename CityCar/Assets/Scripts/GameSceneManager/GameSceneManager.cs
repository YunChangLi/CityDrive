using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LabData;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoSingleton<GameSceneManager>, IGameManager
{
    public GameFlowData FlowData { get; set; }


    private AsyncOperation _operation;

    public Scene CurrentScene { get; private set; }

    public GameSceneResources GameSceneResources { get; private set; }

    private string MainUiScene = "GameUI";




    public void ChangeScene(List<Action> actions)
    {
        
        //场景名
        _operation = null;
        _operation = SceneManager.LoadSceneAsync("VR_scene");
        _operation.completed += (AsyncOperation obj) =>
        {
            OnSceneChangeCompleted();
            actions.ForEach(p => p.Invoke());

        };
    }


    /// <summary>
    /// 加载后做的事儿
    /// </summary>
    private void OnSceneChangeCompleted()
    {
        CurrentScene = SceneManager.GetActiveScene();
        GameSceneResources = FindObjectOfType<GameSceneResources>();
    }


    /// <summary>
    /// 加载UI场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeSceneToMainUi(string sceneName)
    {
        _operation = null;
        _operation = SceneManager.LoadSceneAsync(sceneName);
        //场景加载完后做什么
        _operation.completed += (AsyncOperation obj) =>
        {
            // TODO 加载完场景后做什么，UI做的事儿 UImanager 初始化
            GameUIManager.Instance.StartMainUiLogic();
        };
        _operation.allowSceneActivation = true;
    }


    void IGameManager.ManagerDispose()
    {
        FlowData = null;
        _operation = null;
    }

    void IGameManager.ManagerInit()
    {
        ChangeSceneToMainUi(MainUiScene);

    }
}
