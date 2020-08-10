using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LabData;
using DataSync;
using System;

public class GameDataOut :IData
{
    public string DataName { get; set; }

    public bool IsLoop { get; set; }

    public Func<LabDataBase> Func { get; set; }

    public SaveDataBase SaveData => new SaveDataBase(DataName)
    {
        LabDataBase = () => Func.Invoke()
    };

    public void DataOut()
    {
        LabDataManager.Instance.DataCollect(this, IsLoop);
        
    }

    public GameDataOut(string dataname,Func<LabDataBase> func,bool loop)
    {
        DataName = dataname;
        Func = func;
        IsLoop = loop;
    }

}
