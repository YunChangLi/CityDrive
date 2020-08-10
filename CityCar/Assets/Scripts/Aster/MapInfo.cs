using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    four,
    eight,
}

public class MapInfo
{
    /// <summary>
    /// 矩阵索引表
    /// </summary>
    private MapGridNode[,] mapGridCoordIndexArray;

    /// <summary>
    /// 地图的长
    /// </summary>
    public int mapLength { get; set; }

    /// <summary>
    /// 地图的宽
    /// </summary>
    public int mapWith { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public MoveType moveType = MoveType.four;

    /// <summary>
    /// 实体节点列表
    /// </summary>
    public List<MapGridNode> mapGridNodeList = new List<MapGridNode>();

    /// <summary>
    /// 用来返回实体节点总个数
    /// </summary>
    public int nodeCount { get { return mapGridNodeList.Count; } }

    /// <summary>
    /// 初始化地图数据
    /// </summary>
    /// <param name="maxlength">地图的长</param>
    /// <param name="maxWith">地图的宽</param>
    /// <param name="type">移动类型</param>
    public void SetMapData(int maxlength, int maxWith, MoveType type)
    {
        mapLength = maxlength;
        mapWith = maxWith;  
        moveType = type;
        mapGridCoordIndexArray = new MapGridNode[maxlength, maxWith];
    }

    /// <summary>
    /// 添加实体节点列表
    /// </summary>
    /// <param name="x">横</param>
    /// <param name="y">竖</param>
    /// <param name="node">继承MapGridNode的节点</param>
    public void AddNode(int x, int y, MapGridNode node)
    {  
        //往矩阵索引表中添加矩阵信息
        mapGridCoordIndexArray[x, y] = node;
        //往实体节点列表添加地图
        mapGridNodeList.Add(node);
    }

    /// <summary>
    /// 障碍物替换原先属于自身的节点
    /// </summary>
    /// <param name="x">横</param>
    /// <param name="y">竖</param>
    /// <param name="node">节点</param>
    /// <param name="index">索引值</param>
    public void ChangeNode(int x,int y, MapGridNode node, int index)
    {  
        mapGridCoordIndexArray[x, y] = node;
        mapGridNodeList[index] = node;
    }

    /// <summary>
    /// 根据索引值返回实体
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public MapGridNode GetNodeByIndex(int index)
    {
        return mapGridNodeList[index];
    }

    /// <summary>
    /// 获得节点坐标
    /// </summary>
    /// <param name="coordx">X轴</param>
    /// <param name="coordy">Y轴</param>
    /// <returns></returns>
    public MapGridNode GetNodeByCoord(int coordx,int coordy)
    {
        if (coordx < mapLength
            && coordx >= 0
            && coordy < mapWith
            && coordy >= 0)
        {
            return mapGridCoordIndexArray[coordx, coordy];
        }

        return null;
    }

    /// <summary>
    /// 根据名字返回节点实体
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public MapGridNode GetNodeByName(string name)
    {
        string[] nameArray = name.Split('_');
        return GetNodeByCoord(int.Parse(nameArray[0]), int.Parse(nameArray[1]));
    }


    
}

