using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeSettingPage : BasePage
{
    public InputField RotateSpeedField, MaxSpeedField;

    public Button NextButton, BackButton;

    private void Start()
    {
        NextButton.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Next");
        BackButton.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Back");
        RotateSpeedField.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("RotateSpeed");
        MaxSpeedField.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("MaxSpeed");

        NextButton.onClick.AddListener((() => GoPage(GameUIManager.Instance.MainUi.UiPages.HintPage)));
        BackButton.onClick.AddListener((() => GoPage(GameUIManager.Instance.MainUi.UiPages.MathSettingPage)));
    }

    public override void GoPage(BasePage page)
    {
        GameUIManager.Instance.MainUi.RotateSpeed = float.Parse(string.IsNullOrEmpty(RotateSpeedField.text) ? "20" : RotateSpeedField.text);
        GameUIManager.Instance.MainUi.MaxSpeed = float.Parse(string.IsNullOrEmpty(MaxSpeedField.text) ? "4" : MaxSpeedField.text);
        base.GoPage(page);
    }
}
