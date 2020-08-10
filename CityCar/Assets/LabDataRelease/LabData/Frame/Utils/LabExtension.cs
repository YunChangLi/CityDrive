﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using JsonToCsv;

namespace LabData
{
    public static class LabExtension 
    {
        

        public static string ToJson(this object o)
        {
            return JsonConvert.SerializeObject(o, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
                
            });           
        }

        public static string ToCsv(this object o)
        {
            //todo
            var converter = new JsonToCsvConverter();           
            return converter.Convert(o.ToJson());
        }

        public static string ToTitle(this object o)
        {
            //todo
            var converter = new JsonToCsvConverter();
            return converter.ConvertTitle(o.ToJson());
        }

        public static Pos ToPos(this Vector3 vector3)
        {
            return new Pos()
            {
                X = vector3.x,
                Y = vector3.y,
                Z = vector3.z
            };
        }
        public static QuaternionPos ToQuaternionPos(this Quaternion quaternion)
        {
            return new QuaternionPos()
            {
                X = quaternion.x,
                Y = quaternion.y,
                Z = quaternion.z,
                W = quaternion.w
            };
        }

      
    }

}
