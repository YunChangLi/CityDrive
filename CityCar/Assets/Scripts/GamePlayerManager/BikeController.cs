using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BikeController : MonoBehaviour
{
    public VZController Controller;

    public float yVal { get; set; } = 90;

    public bool IsFadeOut = false;

    private Vector3 initDir;

    private string BuildDate;

    private float rotateSpeed = 20;

    private float maxSpeed = 3;

    const string kReleaseText = "By continuing use of VZfit you agree to the License Agreement at virzoom.com/eula.htm";



    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = GameDataManager.Instance.FlowData.MaxSpeed;
        rotateSpeed = GameDataManager.Instance.FlowData.RotateSpeed;
        BuildDate = DateTime.UtcNow.ToString();
        //VZPlugin.ResetSpeed(maxSpeed);
        //Controller.Restart();
        //Controller.kControllerMaxSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSetup();
        UpdateNormal();
    }

    protected void UpdateSetup()
    {
        // Lookup release screen
        Text release = Controller.TransitionCanvas().transform.Find("Release").GetComponent<Text>();

        // Update calibration msg
        release.text = "Build: " + BuildDate + "\n\n";
        release.text += kReleaseText + "\n\n";

        // Require bike outside of editor
        if (Controller.NeedsDongleDriver())
        {
            release.text += "PLEASE SEE VIRZOOM.COM/DRIVERS";
        }
        else if (!Controller.HasBikeDongle())
        {
#if UNITY_ANDROID
         release.text += "CHECKING PERMISSIONS";
#else
            release.text += "請插入藍芽接收器";
#endif
            Controller.ConnectBike(true, true, true);
        }
        else if (Controller.IsUnsupportedBike())
        {
#if UNITY_ANDROID
         release.text += "DENIED PERMISSIONS";
#else
            release.text += "UNSUPPORTED BIKE";
#endif
        }
        else if (!Controller.IsBikeConnected())
        {
            if (Controller.BikeReprogramProgress() > 0 && Controller.BikeReprogramProgress() < 1.0f)
            {
                release.text += "UPDATING FIRMWARE " + (int)(Controller.BikeReprogramProgress() * 100) + "%";
            }
            else
            {
                release.text += "請試踩踏板";
            }
        }
        else if (!Controller.IsBikeLicensed())
        {
            var msg = Controller.LicenseStatus().ToUpper();

            if (Controller.LicenseUnregistered())
            {
                msg = "Register your sensor kit (" + Controller.BikeSender() + ") at vzfit.com/account";

            }

            release.text += msg;
        }
        else
        {
            release.text += "按下A鍵開始遊戲";
        }

#if UNITY_EDITOR
        if (!Controller.IsBikeConnected())
        {
            if (Controller.ControllerName() == "Keyboard")
                release.text += "\n\n(或按下Enter開始進行模擬)";
            else if (Controller.IsCardboard())
                release.text += "\n\n(or press button to play without bike)";
            else if (Controller.ControllerName() == "GearVR")
                release.text += "\n\n(or press touchpad to play without bike)";
            else if (Controller.ControllerName() == "Gvr")
                release.text += "\n\n(or press touchpad to play without bike)";
            else
                release.text += "\n\n(or press R1 to play without bike)";
        }
#endif

        // Switch to VZButton
        if (Controller.IsBikeSensor() && Controller.ControllerName() != "VZButton")
            Controller.PickController();

        // Hold both buttons to calibrate
        bool pressed = Controller.RightButton.Released();

        if (pressed && Controller.IsHeadTracked() && !IsFadeOut)
        {
            Controller.LeftButton.Clear();
            Controller.RightButton.Clear();

            if (!Controller.IsBikeConnected())
                Controller.CloseBike();

            Controller.Recenter();
            Controller.Restart();
            
            VZPlugin.ResetMotion();
            StartCoroutine(FadeDown(1));
        }
    }

    protected virtual IEnumerator FadeDown(float fadeTime)
    {
        // Fade alpha down to zero
        CanvasGroup group = Controller.TransitionCanvas().GetComponent<CanvasGroup>();
        float time = 0;
        float startAlpha = group.alpha;
        IsFadeOut = true;
        while (time < fadeTime)
        {
            time += VZTime.deltaTime > 0.0f ? VZTime.deltaTime : (1.0f / 60.0f);
            float alpha = Mathf.SmoothStep(startAlpha, 0.0f, time / fadeTime);
            group.alpha = alpha;
            yield return null;
        }

        // Deactivate and reset alpha
        Controller.TransitionCanvas().SetActive(false);
        group.alpha = 1.0f;
    }

    private void UpdateNormal()
    {
        if (!IsFadeOut || !GameTaskManager.Instance.GetComponent<GameStartMode>().isStart) { return; }
        //float speed = Mathf.Abs(Controller.BikeSpeed());
        float speed = Mathf.Abs(Controller.InputSpeed);
        speed = speed > maxSpeed ? maxSpeed : speed;
        Vector3 velocity = transform.forward * speed * Time.deltaTime;
        //Debug.Log(speed);
        // Update camera position
        Controller.Neck().transform.position = transform.position + Vector3.up * 0.85f;
        if (Controller.HeadLean >= 0.1f)
        {
            yVal = yVal - rotateSpeed * Time.deltaTime;
        }
        else if (Controller.HeadLean <= -0.1f)
        {
            yVal = yVal + rotateSpeed * Time.deltaTime;    
        }

        transform.eulerAngles = new Vector3(0, yVal, 0);
        transform.Translate(velocity, Space.World);
    }
}
