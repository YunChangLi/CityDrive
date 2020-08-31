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
        NextButton.onClick.AddListener((() => GoPage(GameUIManager.Instance.MainUi.UiPages.HintPage)));
        BackButton.onClick.AddListener((() => GoPage(GameUIManager.Instance.MainUi.UiPages.MathSettingPage)));
    }

    public override void GoPage(BasePage page)
    {
        GameUIManager.Instance.MainUi.RotateSpeed = float.Parse(RotateSpeedField.text);
        GameUIManager.Instance.MainUi.MaxSpeed = float.Parse(MaxSpeedField.text);
        base.GoPage(page);
    }
}
