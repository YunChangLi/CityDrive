using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapCreater : MonoBehaviour
{
    #region 地图生成参数
    public List<GameObject> NodeObject1;
    public List<GameObject> NodeObject2;
    public List<GameObject> NodeObject3;
    public List<GameObject> NodeObject4;
    public List<GameObject> NodeObject5;
    public List<List<GameObject>> Nodes= new List<List<GameObject>>();
    public MoveType moveType = MoveType.four;
    #endregion

    private MapInfo mapInfo = new MapInfo();
    public bool initMap { get; set; } = false;

    //Singleton
    private static MapCreater instance = null;
    public static MapCreater GetInstance()
    {
        if (null == instance)
        {
            Debug.Log("MapCreater don't have instance!");
        }

        return instance;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void CreaterInit()
    {
        instance = this;
        Nodes.Add(NodeObject1);
        Nodes.Add(NodeObject2);
        Nodes.Add(NodeObject3);
        Nodes.Add(NodeObject4);
        Nodes.Add(NodeObject5);
        //地图加载
        InitMapInfo();
    }

    /// <summary>
    /// 检测地图是否创建完成，创建完成再进行地图数据加载
    /// </summary>
    /// <returns></returns>
    public MapInfo GetMapInfo()
    {
        if (initMap)
        {
            return mapInfo;
        }
        return null;
    }
    /// <summary>
    /// 初始化地图创建集信息加载
    /// </summary>
    void InitMapInfo()
    {
        //生成地图
        CreatMap();
        initMap = true;

        Debug.Log("地图生成成功！");
    }
    /// <summary>
    /// 创造地图
    /// </summary>
    void CreatMap()
    {
        mapInfo.SetMapData(5, 5, moveType);
        for (int x = 0; x < Nodes.Count; x++)
        {
            for (int y = 0; y <Nodes[x].Count; y++)
            {
                List<GameObject> gameObjects = Nodes[x];
                Debug.Log(x +"_" + y);
                FlatNode mapGridNode = new FlatNode(x, y, gameObjects[y].transform.position, gameObjects[y],gameObjects[y].GetComponent<Root>());
                mapInfo.AddNode(x, y, mapGridNode);
            }
        }
    }
   
}
