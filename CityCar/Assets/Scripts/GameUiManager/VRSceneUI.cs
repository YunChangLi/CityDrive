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
    /// 數學運算式UI
    /// </summary>
    public GameObject MathUI;

    /// <summary>
    /// 测试Ui物体
    /// </summary>
    public GameObject ResultUI;

    public Text ResultTimer;
    public Text TrafficlightCount;
    public Text WrongCount;
    public Text MathCount;
    public Text MathCorrectCount;

    public GameObject CorrectImage;
    public GameObject WrongImage;

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
        StringShowType(_remindType, GameExtension.GetCurrentCultureValue(GamePlayerManager.Instance.FlowData.RouteNode.LineText));
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
        ResultTimer.text = GameExtension.GetCurrentCultureValue("ResultTime") + GamePlayerManager.Instance.Timer.ToString() + GameExtension.GetCurrentCultureValue("Second");
        TrafficlightCount.text = GameExtension.GetCurrentCultureValue("TrafficlightCount") + GamePlayerManager.Instance.SignalCount.ToString();
        WrongCount.text = GameExtension.GetCurrentCultureValue("WrongCount") + GamePlayerManager.Instance.WrongCount.ToString();
        MathCount.text = GameExtension.GetCurrentCultureValue("MathCount") + GamePlayerManager.Instance.MathCount.ToString();
        MathCorrectCount.text = GameExtension.GetCurrentCultureValue("MathCorrectCount") + GamePlayerManager.Instance.MathCorrectCount.ToString();
    }

    public void OnTestDisable()
    {
        autoSpeech.StopTip();
        GameSceneManager.Instance.ChangeSceneToMainUi("GameUI");
        GameApplication.Instance.ManagersDispose();
        
    }
}
