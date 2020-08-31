using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Valve.VR.InteractionSystem;

public class MainUI : MonoBehaviour
{
    public DriveEnd UI_DriveEnd { get; set; }

    public DriveLevel UI_DriveLevel { get; set; }

    public RemindType UI_RemindType { get; set; }

    public Language UI_Language { get; set; }

    public string UI_UserID { get; set; }

    public float Frequency { get; set; }

    public float TimeLimit { get; set; }

    public MathDifficulty Difficulty { get; set; }

    public float RotateSpeed { get; set; }

    public float MaxSpeed { get; set; }

    public LineData.Data RouteNode { get; set; }

    public UIPages UiPages;

    private Dictionary<string, Dropdown.OptionData> _dropdowns = new Dictionary<string, Dropdown.OptionData>();

    public GameRouteConfig _routeConfig { get; set; }

    public List<LineData.Data> RouteDatas { get; set; }
    public GameObject Loding;

    public void MainUiInit()
    {
        _routeConfig = GameDataManager.Instance.GetConfig<GameRouteConfig>();
        RouteDatas = _routeConfig.RouteData;
        RouteNode = RouteDatas[0];
        UiPages.LoginPage.PageInit();
    }

    public void StartButton()
    {
        Loding.SetActive(true);
        GameFlowData data = GetData();
        StartGameBtn(data);
    }

    public void StartGameBtn(GameFlowData data)
    {

        GameApplication.Instance.ManagersInit(data);
        MainUiDispose();
    }

    private GameFlowData GetData()
    {
        //UI_DriveLevel = GameUIManager.Instance.MainUi.UI_DriveLevel = (DriveLevel)int.Parse(GameUIManager.Instance.MainUi.RouteDatas[i].Difficult);
        return new GameFlowData(UI_DriveEnd, UI_DriveLevel, UI_RemindType, UI_Language, UI_UserID,RouteNode, Frequency, TimeLimit, Difficulty, RotateSpeed, MaxSpeed);
    }

    private void MainUiDispose()
    {

    }

}
