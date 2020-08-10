using System;
using System.Collections;
using System.Collections.Generic;
using DataSync;
using LabData;
using UnityEngine;

public class LabResultDataDemo : MonoBehaviour,IData
{
    public SaveDataBase SaveData => new SaveDataBase("LabResultDataDemo")
    {
        LabDataBase = ()=> new LabResultDemoData("testResultTest01", "testResultTest02")
    };

  
    void Start()
    {
        LabDataManager.Instance.DataCollect(this, false);
    }

    

}
