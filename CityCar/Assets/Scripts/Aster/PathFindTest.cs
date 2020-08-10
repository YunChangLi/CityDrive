using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 路径查找
/// </summary>
public class PathFindTest:MonoBehaviour
{
    /// <summary>
    /// 地图信息
    /// </summary>
    MapCreater mapCreater = null;

    /// <summary>
    /// 起始点使用时一样的
    /// </summary>
    public FlatNode beginNode { get; set; } = null;

    public List<RouteNode> ResultRoute = new List<RouteNode>(); 

    public void Init(string[] Nodes)
    {
        mapCreater = MapCreater.GetInstance();
        beginNode = mapCreater.GetMapInfo().GetNodeByIndex(0) as FlatNode;
        //判断地图是否加载完成
        if (mapCreater.initMap)
        {
            for (int i = 0; i < Nodes.Length-1; i++)
            {
                Debug.Log(Nodes[i]+"_______"+ Nodes[i + 1]);
                FindStartToEnd(Nodes[i], Nodes[i+1]);
            }
        }
    }



    public void FindStartToEnd(string Start_name,string End_names)
    {
        string[] StartName = Start_name.Split('_');
        string[] EndName = End_names.Split('_');
        PathFindMgr.RequestPathFind(mapCreater.GetMapInfo().GetNodeByCoord(int.Parse(StartName[0]), int.Parse(StartName[1])), mapCreater.GetMapInfo().GetNodeByCoord(int.Parse(EndName[0]), int.Parse(EndName[1])), mapCreater.GetMapInfo(), FindComplete);
    }

    /// <summary>
    /// 用来存入结果信息，委托实现
    /// </summary>
    /// <param name="foundPath"></param>
    void FindComplete(List<AstarNode> foundPath)
    {
        

        for (int i = 0; i < foundPath.Count; i++)
        {
            FlatNode flatNode = mapCreater.GetMapInfo().GetNodeByCoord(foundPath[i].coordx, foundPath[i].coordy) as FlatNode;
            int dif = beginNode.Root.TreeID- flatNode.Root.TreeID;
            Debug.Log(dif);
            if (dif < -1)
            {
                ResultRoute.Add(beginNode.Root.Forward_Child);
                //上孩子
            }
            else if (dif == -1)
            {
                ResultRoute.Add(beginNode.Root.Left_Child);
                //左孩子
            }
            else if (dif == 1)
            {
                //右孩子
                ResultRoute.Add(beginNode.Root.Right_Chlid);
               
            }
            beginNode = flatNode;

            
        }
        
    }
}

