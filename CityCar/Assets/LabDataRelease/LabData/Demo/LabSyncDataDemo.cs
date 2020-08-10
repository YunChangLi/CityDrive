using System;
using System.Collections;
using System.Collections.Generic;
using DataSync;
using UnityEngine;
using LabData;


namespace LabData
{
    public class LabSyncDataDemo : MonoBehaviour,IData
    { 
        public string Id = "Test01";

        public SaveDataBase SaveData => new SaveDataBase("LabSyncDataDemo")
        {
            LabDataBase = ()=>new LabBodyData(i,0,0)
        };



        // Use this for initialization
        void Start()
        {
           LabDataManager.Instance.LabDataCollectInit(Id);
        }

        private int i=1;
        // Update is called once per frame
        void Update()
        {
            i++;

            if (Input.GetKeyUp(KeyCode.A))
            {
                LabDataManager.Instance.DataCollect(this);
            }
            if (Input.GetKeyUp(KeyCode.B))
            {
                LabDataManager.Instance.LabDataDispose();
            }
        }

        void OnDisable()
        {
            LabDataManager.Instance?.LabDataDispose();
        }
       
    }
}

