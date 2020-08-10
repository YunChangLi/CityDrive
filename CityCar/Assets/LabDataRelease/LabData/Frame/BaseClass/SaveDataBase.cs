using System;
using System.Collections;
using System.Collections.Generic;
using DataSync;
using UnityEngine;


namespace LabData
{
    public class SaveDataBase
    {
        public string DataCodeName { get; private set; }

        public Func<LabDataBase> LabDataBase { get; set; }

        public SaveDataBase(string name)
        {
             DataCodeName = name;
        }
       
    }
}


