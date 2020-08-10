using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VRSceneUI : MonoBehaviour
{
    public Text VRSceneText;

    public AutoSpeech autoSpeech;

    /// <summary>
    /// 测试Ui物体
    /// </summary>
    public GameObject ResultUI;

    public Text ResultTimer;
    public Text TrafficlightCount;
    public Text WroingCount;
    

    public void SceneUiInit(/*DriveEnd InitdriveEnd, /*DriveLevel InitdriveLevel,*/ RemindType InitremindType)
    {
        TextShow(/*InitdriveEnd,InitdriveLevel,*/InitremindType);
    }

    /// <summary>
    /// 根据选项显示UI
    /// </summary>
    /// <param name="driveEnd"></param>
    /// <param name="driveLevel"></param>
    public void TextShow(/*DriveEnd driveEnd,DriveLevel driveLevel,*/RemindType _remindType)
    {
        StringShowType(_remindType, GamePlayerManager.Instance.FlowData.RouteNode.LineText);
    }

    /// <summary>
    /// 音频播放
    /// </summary>
    /// <param name="remindType"></param>
    /// <param name="text"></param>
    private void StringShowType(RemindType remindType,string text )
    {
        switch (remindType)
        {
            case RemindType.Text:
                VRSceneText.text = text;
                break;
            case RemindType.Voice:
                autoSpeech.CheckoutTips(text);
                break;
            case RemindType.Text_Voice:
                VRSceneText.text = text;
                autoSpeech.CheckoutTips(text);
                break;
        }
    }

    public void UiResult()
    {
        ResultUI.SetActive(true);
        UpdateResultUI();
    }

    public void UpdateResultUI()
    {
        ResultTimer.text = "测试用时:" + GamePlayerManager.Instance.Timer.ToString()+"秒";
        TrafficlightCount.text = "闯红灯次数:" + GamePlayerManager.Instance.SignalCount.ToString()+"次";
        WroingCount.text = "错误路线次数:" + GamePlayerManager.Instance.WrongCount.ToString()+"次";
    }

    public void OnTestDisable()
    {
        autoSpeech.StopTip();
        GameSceneManager.Instance.ChangeSceneToMainUi("GameUI");
        GameApplication.Instance.ManagersDispose();
        
    }
}
