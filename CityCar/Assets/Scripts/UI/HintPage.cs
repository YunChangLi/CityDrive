using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HintPage : BasePage
{

    public Button TextBtn;
    public Button VoiceBtn;
    public Button Text_VoiceBtn;
    public GameObject Loding;


    void Start()
    {

        TextBtn.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Remaind_Text");
        VoiceBtn.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Remaind_Voice");
        Text_VoiceBtn.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Remaind_Text_Voice");


        TextBtn.onClick.AddListener(() =>
        {
            GameUIManager.Instance.MainUi.UI_RemindType = RemindType.Text;
            Debug.Log("re文字提示");
            GameUIManager.Instance.MainUi.StartButton();
        });

        VoiceBtn.onClick.AddListener(() =>
        {
            GameUIManager.Instance.MainUi.UI_RemindType = RemindType.Voice;
            Debug.Log("re聲音提示");
            GameUIManager.Instance.MainUi.StartButton();
        });

        Text_VoiceBtn.onClick.AddListener(() =>
        {
            GameUIManager.Instance.MainUi.UI_RemindType = RemindType.Text_Voice;
            Debug.Log("re文字&聲音提示");
            GameUIManager.Instance.MainUi.StartButton();
        });


    }

    public override void GoPage(BasePage page)
    {
        base.GoPage(page);
    }

}
