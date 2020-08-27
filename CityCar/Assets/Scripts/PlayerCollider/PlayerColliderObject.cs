using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Player 所有碰撞事件
/// </summary>
public class PlayerColliderObject : MonoBehaviour
{
    /// <summary>
    /// 判断是都逆行
    /// </summary>
    private bool _isRotation=false;
    /// <summary>
    /// 判断是否在公共区域
    /// </summary>
    private bool _isPublic=true;
    /// <summary>
    /// 当前节点
    /// </summary>
    private RouteNode routeNode;
    /// <summary>
    /// 红绿灯
    /// </summary>
    private TrafficlightController trafficlight;

    /// <summary>
    /// 路径
    /// </summary>
    public PathFindTest path { get; set; }

    public Rigidbody Rigidbody { get; set; }

    /// <summary>
    /// 路径索引值
    /// </summary>
    private int index=0;

    /// <summary>
    /// 是否完成
    /// </summary>
    private bool isOver = false;

    public bool GameSceneInitOver { get; set; }

    private float _timer;

    /// <summary>
    /// 玩家起始位置
    /// </summary>
    private Vector3 StartPos;

    private Quaternion Startquaternion;

    /// <summary>
    /// 
    /// </summary>
    public Camera MainCamera;


    public AutoSpeech AutoSpeech;

    /// <summary>
    /// 初始化
    /// </summary>
    private void Awake()
    {
       
        StartPos = this.gameObject.transform.position;
        Startquaternion = Quaternion.Euler(new Vector3(0, 90, 0)); 
        Rigidbody = this.gameObject.GetComponent<Rigidbody>();

    }

    private void Start()
    {
        MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        GetComponentInChildren<Canvas>().worldCamera = MainCamera;
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        if (isOver == false&&GameSceneInitOver==true)
        {
            //判断当前节点信息，自身方向
            if (_isRotation)
            {

                float playerRot = GetInspectorRotationValueMethod(this.transform);
                float routeRot = GetInspectorRotationValueMethod(routeNode.transform);
                if ((playerRot < (routeRot - 90)) || (playerRot > (routeRot + 90))&&(routeNode.RouteNodeID!=8&& routeNode.RouteNodeID != 10))
                {
                    //TODO  屏幕变黑将玩家移回起点
                    Debug.Log("重置位置");
                    StartCoroutine(ResetTransform(GameTaskManager.Instance.UesrInput));
                }
            }

            //判断是否离开公共区域和马路
            if (!_isPublic && !_isRotation)
            {
                GamePlayerManager.Instance.WrongCount += 1;
                //TODO  屏幕变黑将玩家移回起点
                Debug.Log("离开路线区域");
                StartCoroutine(ResetTransform(GameTaskManager.Instance.UesrInput));
            }
        }

        _timer += Time.deltaTime;
    }

    /// <summary>
    /// 进入碰撞体
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (isOver == true)
            return;
        
        //如果碰到了路段
        if (other.tag == "Route")
        {
           
            routeNode = other.GetComponent<RouteNode>();
            if (routeNode.RouteNodeID == path.ResultRoute[index].RouteNodeID)
            {
                _isRotation = true;
                index += 1;
                Debug.Log("走对了");
                if (routeNode.RouteNodeID == path.ResultRoute[path.ResultRoute.Count - 1].RouteNodeID)
                {
                    StartCoroutine(GameOver());
                }
            }
            else 
            {
                GamePlayerManager.Instance.WrongCount += 1;
                //TODO 走错了路
                Debug.Log("走错了路");
                StartCoroutine( ResetTransform(GameTaskManager.Instance.UesrInput));
            }
        }
        else if (other.tag == "Publicareas")
        {
            _isPublic = true;
        }
        else if (other.tag == "Trafficlight")
        {
            Debug.Log(111111);
            trafficlight = other.GetComponent<TrafficlightController>();
            StartCoroutine(trafficlight.TrafficlightTimer());
        }

    }
    /// <summary>
    /// 离开碰撞体
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (isOver == true)
            return;

        if (other.tag == "Route")
        {
            _isRotation = false;
            if (trafficlight != null)
            {
                if (trafficlight.isRedLight)
                {
                    GamePlayerManager.Instance.SignalCount += 1;
                    Debug.Log("闯红灯");
                }
            }
        }
        else if (other.tag == "Publicareas")
        {
            _isPublic = false;
        }
    }
    /// <summary>
    /// 四元数转实际值
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public float GetInspectorRotationValueMethod(Transform transform)
    {
        // 获取原生值
        System.Type transformType = transform.GetType();
        PropertyInfo m_propertyInfo_rotationOrder = transformType.GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);
        object m_OldRotationOrder = m_propertyInfo_rotationOrder.GetValue(transform, null);
        MethodInfo m_methodInfo_GetLocalEulerAngles = transformType.GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
        object value = m_methodInfo_GetLocalEulerAngles.Invoke(transform, new object[] { m_OldRotationOrder });
        string temp = value.ToString();
        //将字符串第一个和最后一个去掉
        temp = temp.Remove(0, 1);
        temp = temp.Remove(temp.Length - 1, 1);
        //用‘，’号分割
        string[] tempVector3;
        tempVector3 = temp.Split(',');
        //将分割好的数据传给Vector3
        Vector3 vector3 = new Vector3(float.Parse(tempVector3[0]), float.Parse(tempVector3[1]), float.Parse(tempVector3[2]));
        return vector3.y;
    }
    /// <summary>
    /// 重置位置
    /// </summary>
    /// <returns></returns>
    public IEnumerator ResetTransform(Func<bool> Input)
    {
        AutoSpeech.CheckoutTips("您已偏航");
        GameTaskManager.Instance.GetComponent<GameStartMode>().StopMath();
        isOver = true;
        //rCC.enabled = false;
        
        yield return new WaitForSeconds(2);
        
        //相机黑屏
        MainCamera.clearFlags = CameraClearFlags.Color;
        MainCamera.cullingMask = (1 << 1);
        Rigidbody.isKinematic = true; //速度归零
        this.gameObject.transform.position = StartPos;
        this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        GetComponent<BikeController>().yVal = 90;
        Debug.Log(StartPos);
        this.gameObject.transform.rotation = Startquaternion;
        //相机正常
        yield return new WaitForSeconds(2);

        


        MainCamera.clearFlags = CameraClearFlags.Skybox;
        MainCamera.cullingMask =-1;
        index = 0;


        //TODO 错误路线计数
        //rCC.enabled = true;
        //rCC.speed = 0;
        isOver = false;
        _isPublic = true;
        AutoSpeech.CheckoutTips("重置完毕");
        Debug.Log("重置完毕");

        GameUIManager.Instance.UiShowText();

        yield return new WaitUntil(Input);

        GameUIManager.Instance.VRSceneUI.VRSceneText.text = "";
        //rCC.speed = GameTaskManager.Instance.GameTaskConfig.Speed;
        Rigidbody.isKinematic = false;

        GameTaskManager.Instance.GetComponent<GameStartMode>().StartMath();
    }
    /// <summary>
    /// 游戏结束
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        isOver = true;
        AutoSpeech.CheckoutTips("恭喜您完成任务");
        GamePlayerManager.Instance.Timer = _timer;
        GameUIManager.Instance.VRSceneUI.UiResult();
        float i = 0.0f;
        //rCC.IsStop = true;
        yield return new WaitForSeconds(2);
        GamePlayerManager.Instance.PlayerGameDataOut.ResultDataOut();
    }
    public bool GetIsOver() 
    {
        return isOver;
    }

    public void OnDisableCollider()
    {
        StopAllCoroutines();
    }

}
