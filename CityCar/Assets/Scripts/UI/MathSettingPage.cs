using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathSettingPage : BasePage, ILanguageTranslate
{
    public Button NextButton, BackButton;

    public InputField FrequencyField, TimeLimitField;

    public Dropdown DifficultyDropdown;

    private string easy, normal, hard;

    void Start()
    {
        NextButton.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Next");
        BackButton.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Back");
        FrequencyField.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Frequency");
        TimeLimitField.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("TimeLimit");

        NextButton.onClick.AddListener((() => GoPage(GameUIManager.Instance.MainUi.UiPages.BikeSettingPage)));
        BackButton.onClick.AddListener((() => GoPage(GameUIManager.Instance.MainUi.UiPages.RoutePage)));

        easy = GameExtension.GetCurrentCultureValue("Easy");
        normal = GameExtension.GetCurrentCultureValue("Normal");
        hard = GameExtension.GetCurrentCultureValue("Hard");
        List<string> diffList = new List<string>();
        diffList.Add(easy);
        diffList.Add(normal);
        diffList.Add(hard);
        DifficultyDropdown.AddOptions(diffList);
    }

    public override void GoPage(BasePage page)
    {
        GameUIManager.Instance.MainUi.Frequency = int.Parse(FrequencyField.text);
        GameUIManager.Instance.MainUi.TimeLimit = int.Parse(TimeLimitField.text);

        if(DifficultyDropdown.captionText.text.ToString() == easy)
        {
            GameUIManager.Instance.MainUi.Difficulty = MathDifficulty.Easy;
        }
        if (DifficultyDropdown.captionText.text.ToString() == normal)
        {
            GameUIManager.Instance.MainUi.Difficulty = MathDifficulty.Normal;
        }
        if (DifficultyDropdown.captionText.text.ToString() == hard)
        {
            GameUIManager.Instance.MainUi.Difficulty = MathDifficulty.Hard;
        }
        base.GoPage(page);
    }

    public void LanguageTraslate()
    {

    }
}
