using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabVisualization;
using LabVisualization.EyeTracing;
using System;
using ViveSR.anipal.Eye;
using LabData;
using DataSync;

public class EyeTrackEquipment : MonoBehaviour, IEquipment, IEyeTracingPos
{
    public SRanipal_Eye_Framework Sranipal { get; set; }

    private bool IsOpenEye = false;

    public Vector2 EyeData { get; set; }

    SingleEyeData LeftData { get; set; }
    SingleEyeData RightData { get; set; }
    SingleEyeData Combined { get; set; }

    public IEnumerator Enumerator { get; set; }


    public void EquipmentInit()
    {

        if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING)
        {

            if (Sranipal == null)
            {
                Sranipal = gameObject.AddComponent<SRanipal_Eye_Framework>();
            }
            Sranipal = SRanipal_Eye_Framework.Instance;

            Sranipal.StartFramework();
            IsOpenEye = true;
        }

    }

    public void EquipmentStart()
    {
        if (IsOpenEye)
        {
            Enumerator = UpdateData();
            StartCoroutine(Enumerator);
        }
    }

    public void EquipmentStop()
    {
        if (IsOpenEye)
        {
            Sranipal.StopFramework();
            IsOpenEye = false;
            if (Enumerator != null)
            {
                StopCoroutine(Enumerator);
            }
        }
    }

    public Func<Vector2> GetEyeTracingPos()
    {
        return () => EyeData;
    }

    private IEnumerator UpdateData()
    {
        while (true)
        {

            if (SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.WORKING)
            {
                VerboseData data;
                if (SRanipal_Eye.GetVerboseData(out data) &&
                    data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_DIRECTION_VALIDITY) &&
                    data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_DIRECTION_VALIDITY)
                    )
                {
                    EyeData = data.left.pupil_position_in_sensor_area;
                    LeftData = data.left;
                    RightData = data.right;
                    Combined = data.combined.eye_data;
                }
            }
            yield return new WaitForFixedUpdate();

        }
    }


}
