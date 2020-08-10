using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;
using System;
using DataSync;

public class PlayerGameDataOut : MonoBehaviour
{
    /// <summary>
    /// 汽车位置坐标
    /// </summary>
    private Pos  _carPos;
    /// <summary>
    /// 头动位置
    /// </summary>
    private QuaternionPos _playerHead;
    /// <summary>
    /// 眼动位置
    /// </summary>
    private Vector2 _playerEye;
    ///// <summary>
    ///// 开启数据读写
    ///// </summary>
    public void LoopDataOut()
    {
        GameDataOut CarData = new GameDataOut("CarData", DriveCarDatas, true);
        GameDataOut HeadData = new GameDataOut("HeadData", PlayerHeadDatas, true);
        GameDataOut EyeData = new GameDataOut("EyeData", PlayerEyeDatas, true);

        CarData.DataOut();
        HeadData.DataOut();
        EyeData.DataOut();

    }

    public void ResultDataOut()
    {
        GameDataOut ResultData = new GameDataOut("ResultData", GameResultDatas, false);
        ResultData.DataOut();
    }

    private void Update()
    {
        _carPos = GamePlayerManager.Instance.Player.transform.position.ToPos();
        _playerHead = GamePlayerManager.Instance.PlayerColliderObject.MainCamera.transform.rotation.ToQuaternionPos();
        _playerEye = new Vector2(0, 0);
    }

    public DriveCarData DriveCarDatas()
    {
        return new DriveCarData(_carPos.X, _carPos.Y, _carPos.Z);
    }

    public PlayerHeadData PlayerHeadDatas()
    {
        return new PlayerHeadData(_playerHead.X, _playerHead.Y, _playerHead.Z , _playerHead.W);
    }

    public PlayerEyeData PlayerEyeDatas()
    {
        return new PlayerEyeData(_playerEye.x, _playerEye.y,0);
    }

    public GameResultData GameResultDatas()
    {
        return new GameResultData(GamePlayerManager.Instance.Timer, GamePlayerManager.Instance.SignalCount, GamePlayerManager.Instance.WrongCount);
    }
}
