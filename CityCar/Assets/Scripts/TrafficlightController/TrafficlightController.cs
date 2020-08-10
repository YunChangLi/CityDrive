using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrafficlightController : MonoBehaviour
{
    public Material YellowLight;

    /// <summary>
    /// 红灯材质
    /// </summary>
    public Material RedLight;
    /// <summary>
    /// 绿灯材质
    /// </summary>
    public Material GreenLight;
    /// <summary>
    /// 需要变红的灯
    /// </summary>
    public List<MeshRenderer> NeedRedLight;
    /// <summary>
    /// 需要变绿的灯
    /// </summary>
    public List<MeshRenderer> NeedGreenLight;
    /// <summary>
    /// 是否为红灯
    /// </summary>
    public bool isRedLight { get; set; }


    private void ChangeYellow()
    {
        Debug.Log("黄灯亮");
        NeedRedLight.ForEach(p => p.materials[0].CopyPropertiesFromMaterial(YellowLight));
        NeedGreenLight.ForEach(p => p.materials[0].CopyPropertiesFromMaterial(GreenLight));
        isRedLight = true;
    }

    private void ChangeRed()
    {
        Debug.Log("红灯亮");
        NeedRedLight.ForEach(p=>p.materials[0].CopyPropertiesFromMaterial(RedLight));
        NeedGreenLight.ForEach(p => p.materials[0].CopyPropertiesFromMaterial(GreenLight));
        isRedLight = true;
    }



    private void ChangeGreen()
    {
        Debug.Log("绿灯亮");
        NeedRedLight.ForEach(p => p.materials[0].CopyPropertiesFromMaterial(GreenLight));
        NeedGreenLight.ForEach(p => p.materials[0].CopyPropertiesFromMaterial(RedLight));
        isRedLight = false;
    }



    public IEnumerator TrafficlightTimer()
    {
        ChangeYellow();
        yield return new WaitForSeconds(1);
        ChangeRed();
        yield return new WaitForSeconds(GameTaskManager.Instance.GameTaskConfig.RedGreenLightTimer);
        ChangeGreen();
        yield return null;
    }
}
