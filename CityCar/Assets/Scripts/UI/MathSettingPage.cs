using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathSettingPage : BasePage, ILanguageTranslate
{
    public Button NextButton, BackButton;

    public InputField FrequencyField, TimeLimitField;

    public Dropdown DifficultyDropdown;

    void Start()
    {
        NextButton.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Next");
        BackButton.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Back");
        FrequencyField.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("Frequency");
        TimeLimitField.GetComponentInChildren<Text>().text = GameExtension.GetCurrentCultureValue("TimeLimit");

        NextButton.onClick.AddListener((() => GoPage(GameUIManager.Instance.MainUi.UiPages.HintPage)));
        BackButton.onClick.AddListener((() => GoPage(GameUIManager.Instance.MainUi.UiPages.RoutePage)));
    }

    public override void GoPage(BasePage page)
    {
        GameUIManager.Instance.MainUi.Frequency = int.Parse(FrequencyField.text);
        GameUIManager.Instance.MainUi.TimeLimit = int.Parse(TimeLimitField.text);

        switch (DifficultyDropdown.captionText.text)
        {
            case "Easy":
                GameUIManager.Instance.MainUi.Difficulty = MathDifficulty.Easy;
                break;
            case "Normal":
                GameUIManager.Instance.MainUi.Difficulty = MathDifficulty.Normal;
                break;
            case "Hard":
                GameUIManager.Instance.MainUi.Difficulty = MathDifficulty.Hard;
                break;
            default:
                break;
        }
        base.GoPage(page);
    }

    public void LanguageTraslate()
    {

    }
}
