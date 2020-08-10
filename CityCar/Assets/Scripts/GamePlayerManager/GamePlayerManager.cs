using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;
using DataSync;

public class GamePlayerManager : MonoSingleton<GamePlayerManager>, IGameManager
{
    public GameFlowData FlowData { get; set; }
    /// <summary>
    /// 持有玩家对象
    /// </summary>
    public GameObject Player { get; set; }
    /// <summary>
    /// 持有碰撞事件脚本
    /// </summary>
    public PlayerColliderObject PlayerColliderObject { get; set; }
    /// <summary>
    /// 持有玩家路线规划脚本
    /// </summary>
    public PathFindTest PathFindTest { get; set; }
    /// <summary>
    /// MapInit
    /// </summary>
    public MapCreater MapCreater { get; set; }
    /// <summary>
    /// 游戏总时
    /// </summary>
    public float Timer { get; set; }
    /// <summary>
    /// 信号灯违章次数
    /// </summary>
    public int SignalCount { get; set; }
    /// <summary>
    /// 错误路线此使
    /// </summary>
    public int WrongCount { get; set; }

    public PlayerGameDataOut PlayerGameDataOut {get;set;}

   /// <summary>
   /// 玩家初始化
   /// </summary>
    public void PlayerInit()
    {
        //持有脚本
        Player = GameObject.Find("Player");
        PathFindTest = GameObject.Find("PathFind").GetComponent<PathFindTest>();
        MapCreater = GameObject.Find("MapCreater").GetComponent<MapCreater>();
        PlayerColliderObject = Player.GetComponent<PlayerColliderObject>();
        PlayerGameDataOut = this.gameObject.AddComponent<PlayerGameDataOut>();
        
    }
    /// <summary>
    /// 玩家开始
    /// </summary>
    public void PlayerStart()
    {
        //地图初始化
        MapCreater.CreaterInit();
        //路径初始化
        PathFindTest.Init(GetRouteNode());
        //赋值路径
        PlayerColliderObject.path = PathFindTest;
        //告诉玩家身上的碰撞脚本为True；
        PlayerColliderObject.GameSceneInitOver = true;
        PlayerGameDataOut.LoopDataOut();
    }
 
    /// <summary>
    /// 解析路线
    /// </summary>
    /// <returns></returns>
    private string[] GetRouteNode()
    {
        return FlowData.RouteNode.Route.Split(',');

    }
    
    public void ManagerDispose()
    {
        Timer = 0;
        SignalCount = 0;
        WrongCount = 0;
        Player = null;
        PathFindTest = null;
        MapCreater = null;
        PlayerColliderObject = null;
        Destroy(this.gameObject.GetComponent<PlayerGameDataOut>());
        FlowData = null;

    }

    public void ManagerInit()
    {
        
    }

   
}
