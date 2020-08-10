using UnityEngine;

public class FlatNode : MapGridNode
{
    
    private bool m_isBegin;

    public Root Root;

    /// <summary>
    /// 该店是否为起点
    /// </summary>
    public bool isBegin
    {
        set
        {
            m_isBegin = value;
        }
    }
    /// <summary>
    /// 单个地图属性实例化实例化
    /// </summary>
    /// <param name="x">横</param>
    /// <param name="y">竖</param>
    /// <param name="pos">位置</param>
    /// <param name="gridObj">相对应的实体</param>
    public FlatNode(int x, int y, Vector3 pos, GameObject gridObj,Root root)
        : base(x, y, pos, gridObj)
    {
        Root = root;
        canWalk = true;
        type = MapNodeType.flat;
    }
}

