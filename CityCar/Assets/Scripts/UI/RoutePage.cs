using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoutePage : BasePage , ILanguageTranslate
{
    public Button NextButton , BackButton;

    [SerializeField]
    private GameObject _dropdownObj;

    [SerializeField]
    private RectTransform _endDropdownRectTransform;
    [SerializeField]
    private RectTransform _levelDropdownRectTransform;



    void Start()
    {
       

        NextButton.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Next");
        BackButton.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Back");

        DropDownAddOption(_endDropdownRectTransform, GameUIManager.Instance.MainUi.RouteDatas);

        NextButton.onClick.AddListener((() => GoPage(GameUIManager.Instance.MainUi.UiPages.HintPage)));
        BackButton.onClick.AddListener(()=>GoPage(GameUIManager.Instance.MainUi.UiPages.LoginPage));

        GameUIManager.Instance.MainUi.RouteNode = GameUIManager.Instance.MainUi.RouteDatas[0];
        GameUIManager.Instance.MainUi.UI_DriveLevel = (DriveLevel)int.Parse(GameUIManager.Instance.MainUi.RouteDatas[0].Difficult);

    }

    /// <summary>
    /// 將Enum做成選項
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public GameObject DropDownAddOption(RectTransform parent,List<LineData.Data> datas)
    {
        var tempDrop = Instantiate(_dropdownObj, parent);

        foreach (LineData.Data value in datas)
        {
            tempDrop.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData(GameExtension.GetCurrentCultureValue(value.LineName)));
        }
        tempDrop.GetComponent<Dropdown>().onValueChanged.AddListener(OnDropDownValueChange);
        tempDrop.GetComponent<Dropdown>().RefreshShownValue();

        return tempDrop;
    }

    private void OnDropDownValueChange(int i)
    {
        GameUIManager.Instance.MainUi.RouteNode = GameUIManager.Instance.MainUi.RouteDatas[i];
        GameUIManager.Instance.MainUi.UI_DriveLevel = (DriveLevel)int.Parse(GameUIManager.Instance.MainUi.RouteDatas[i].Difficult);
    }

    public override void GoPage(BasePage page)
    {
        base.GoPage(page);
    }

    public void LanguageTraslate()
    {

    }

}
