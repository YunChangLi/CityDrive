using System.Collections;
using System.Collections.Generic;
using System.IO;
using LabData;
using Newtonsoft.Json;
using UnityEngine;
using System.Runtime.Serialization;
using System.Web;


public class GameDataManager : MonoSingleton<GameDataManager>, IGameManager
{

    public T GetData<T>(GameFlowData data) where T : ScriptableObject
    {
        
        return null;
    }

   


    public T GetConfig<T>() where T : new()
    {
        var path = Application.streamingAssetsPath + "/" + typeof(T).Name + ".json";
        if (!File.Exists(path))
        {
            var json = JsonConvert.SerializeObject(new T());
            var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(json);
            sw.Close();
        }

        StreamReader sr = new StreamReader(path);
        return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
    }

    public GameFlowData FlowData { get; set; }

    public void LabDataInit()
    {
        LabDataManager.Instance.LabDataCollectInit(FlowData.UserID);
    }




    public void ManagerInit()
    {
        
    }

    public void ManagerDispose()
    {
        FlowData = null;
        LabDataManager.Instance.LabDataDispose();
    }

    public string UserID => FlowData.UserID;
}
