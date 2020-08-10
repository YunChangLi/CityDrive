using System;
using System.Collections.Generic;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPage : BasePage
{
    public Button StartButton , ExitButton;

    public InputField InputField;

    //private Dictionary<string, Dropdown.OptionData> _dropdowns = new Dictionary<string, Dropdown.OptionData>();

    void Start()
    {
        StartButton.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Start");
        ExitButton.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Exit");
        InputField.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("USERID");

        StartButton.onClick.AddListener((() => GoPage(GameUIManager.Instance.MainUi.UiPages.RoutePage)));
        ExitButton.onClick.AddListener((()=>QuitGame()));
    }

    public override void GoPage(BasePage page)
    {
        Debug.Log("點擊開始按鈕");
        GameUIManager.Instance.MainUi.UI_UserID = InputField.text;
        Debug.Log("回傳 userid");
        base.GoPage(page);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
