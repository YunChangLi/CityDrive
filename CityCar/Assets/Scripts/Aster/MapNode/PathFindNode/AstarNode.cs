
/// <summary>
/// 寻路节点，权重值计算相关
/// </summary>
public class AstarNode: NodeBase
{
    /// <summary>
    /// 表示当前点到终点的代价
    /// </summary>
    public float H;
    /// <summary>
    /// 表示当前点到起始点的估量代价
    /// </summary>
    public float G;
    /// <summary>
    /// 
    /// </summary>
    public float F
    {
        get
        {
            return G + H;
        }
    }

    public AstarNode parentNode; 
    public AstarNode(int x, int y)
    {
        coordx = x;
        coordy = y;
    }
}

