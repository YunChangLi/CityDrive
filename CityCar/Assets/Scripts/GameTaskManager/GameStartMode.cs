using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStartMode : MonoBehaviour,IPlayerStartTest
{
    public void EndLogic()
    {
        
    }

    public IEnumerator StartGameLogic(Func<bool> Input)
    {
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
        GamePlayerManager.Instance.PlayerColliderObject.rCC.enabled = true;
        GamePlayerManager.Instance.PlayerColliderObject.rCC.speed = GameTaskManager.Instance.GameTaskConfig.Speed;

        //开启汽车驾驶功能
        GamePlayerManager.Instance.PlayerStart();
    }

    
}
