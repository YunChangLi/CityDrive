using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameStartMode : MonoBehaviour, IPlayerStartTest
{

    private float startTime;
    private List<string> exp = new List<string>();
    public bool isStart = false;

    private IEnumerator MathCoroutine;


    public void EndLogic()
    {

    }

    public IEnumerator StartGameLogic(Func<bool> Input)
    {
        isStart = false;
        yield return new WaitUntil(() => FindObjectOfType<BikeController>().IsFadeOut);
        //关闭汽车组件
        //GamePlayerManager.Instance.PlayerColliderObject.rCC.enabled = false;

        //显示指导语UI
        GameUIManager.Instance.UiShowText();

        //等待用户按按钮
        yield return new WaitUntil(Input);

        //等待按键，让UI消失
        isStart = true;
        GameUIManager.Instance.VRSceneUI.VRSceneText.text = "";
        GameUIManager.Instance.VRSceneUI.autoSpeech.StopTip();
        //开启汽车组件
        //GamePlayerManager.Instance.PlayerColliderObject.rCC.enabled = true;
        //GamePlayerManager.Instance.PlayerColliderObject.rCC.speed = GameTaskManager.Instance.GameTaskConfig.Speed;

        //开启汽车驾驶功能
        GamePlayerManager.Instance.PlayerStart();

        StartMath();
    }

    public void StartMath()
    {
        MathCoroutine = Math();
        StartCoroutine(MathCoroutine);
    }

    public void StopMath()
    {
        GameUIManager.Instance.VRSceneUI.MathUI.SetActive(false);
        StopCoroutine(MathCoroutine);
    }

    public IEnumerator Math()
    {
        GameObject mathUI = GameUIManager.Instance.VRSceneUI.MathUI;
        ExpressionCreator creator = mathUI.GetComponent<ExpressionCreator>();
        creator.OperatorNumber = (int)GameDataManager.Instance.FlowData.Difficulty;
        while (!FindObjectOfType<PlayerColliderObject>().GetIsOver())
        {
            yield return new WaitForSeconds(GameDataManager.Instance.FlowData.Frequency);
            startTime = Time.time;

            exp = creator.CreateExpression();
            string showExp = "";
            foreach (string str in exp)
            {
                showExp += str;
            }

            int correctAnswer = creator.GetAnswer(exp);
            int wrongAnswer = creator.GetWrongAns(correctAnswer);
            int correct = UnityEngine.Random.Range(0, 2);
            mathUI.SetActive(true);
            if (correct == 0)
            {
                mathUI.GetComponentInChildren<Text>().text = showExp + " = " + wrongAnswer.ToString();
            }
            else
            {
                mathUI.GetComponentInChildren<Text>().text = showExp + " = " + correctAnswer.ToString();
            }
            yield return new WaitUntil(() => Time.time - startTime > GameDataManager.Instance.FlowData.TimeLimit || ChooseAnswer(correct) || FindObjectOfType<PlayerColliderObject>().GetIsOver());
            GamePlayerManager.Instance.MathCount++;
            //yield return new WaitForSeconds(GameDataManager.Instance.FlowData.TimeLimit);
            mathUI.SetActive(false);
        }

        if(startTime + GameDataManager.Instance.FlowData.TimeLimit > Time.time)
        {
            GamePlayerManager.Instance.MathCount--;
        }
    }

    public bool ChooseAnswer(int correct)
    {
        if (FindObjectOfType<VZController>().RightButton.Pressed())
        {
            if(correct == 1)
            {
                GamePlayerManager.Instance.MathCorrectCount++;
                StartCoroutine(FadeDown(2, GameUIManager.Instance.VRSceneUI.CorrectImage));
            }
            else
            {
                StartCoroutine(FadeDown(2, GameUIManager.Instance.VRSceneUI.WrongImage));
            }
            return true;
        }
        else if (FindObjectOfType<VZController>().LeftButton.Pressed())
        {
            if (correct == 1)
            {
                StartCoroutine(FadeDown(2, GameUIManager.Instance.VRSceneUI.WrongImage));
            }
            else
            {
                GamePlayerManager.Instance.MathCorrectCount++;
                StartCoroutine(FadeDown(2, GameUIManager.Instance.VRSceneUI.CorrectImage));
            }
            return true;
        }
        return false;
    }

    protected virtual IEnumerator FadeDown(float fadeTime, GameObject image)
    {
        image.SetActive(true);
        // Fade alpha down to zero
        float time = 0;

        while (time < fadeTime)
        {
            time += VZTime.deltaTime > 0f ? VZTime.deltaTime : (1f / 60f);
            float alpha = Mathf.SmoothStep(1f, 0f, time / fadeTime);
            image.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // Deactivate and reset alpha
        image.SetActive(false);
        image.GetComponent<Image>().color = Color.white;
    }
}
