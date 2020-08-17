using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameStartMode : MonoBehaviour,IPlayerStartTest
{
    public void EndLogic()
    {
        
    }

    public IEnumerator StartGameLogic(Func<bool> Input)
    {
        yield return new WaitUntil(() => FindObjectOfType<VZPlayer>().IsFadeOut);
        //关闭汽车组件
        //GamePlayerManager.Instance.PlayerColliderObject.rCC.enabled = false;
        
        //显示指导语UI
        GameUIManager.Instance.UiShowText();

        //等待用户按按钮
        yield return new  WaitUntil(Input);

        //等待按键，让UI消失
        GameUIManager.Instance.VRSceneUI.VRSceneText.text = "";
        GameUIManager.Instance.VRSceneUI.autoSpeech.StopTip();
        //开启汽车组件
        //GamePlayerManager.Instance.PlayerColliderObject.rCC.enabled = true;
        //GamePlayerManager.Instance.PlayerColliderObject.rCC.speed = GameTaskManager.Instance.GameTaskConfig.Speed;

        //开启汽车驾驶功能
        GamePlayerManager.Instance.PlayerStart();

        GameObject mathUI = GameUIManager.Instance.VRSceneUI.MathUI;
        while (true)
        {
            yield return new WaitForSeconds(GameDataManager.Instance.FlowData.Frequency);
            string exp = mathUI.GetComponent<ExpressionCreator>().GetExpression();
            mathUI.SetActive(true);
            mathUI.GetComponentInChildren<Text>().text = exp;
            yield return new WaitForSeconds(GameDataManager.Instance.FlowData.TimeLimit);
            mathUI.SetActive(false);
        }
    }

    
}
