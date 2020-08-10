using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace LabData
{
    public class LabTools
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="floderName"></param>
        /// <param name="isNew"></param>
        public static string CreatFolder(string floderName,bool isNew=false)
        {

            if (Directory.Exists(floderName))
            {
                if (isNew)
                {
                    var tempPath = floderName + "_" + DateTime.Now.Millisecond.ToString();
                    Directory.CreateDirectory(tempPath);
                    return tempPath;
                }
                Debug.Log("Folder Has Existed!");
                return floderName;
                
            }
            else
            {
                Directory.CreateDirectory(floderName);
                Debug.Log("Success Create" + floderName);
                return floderName;
            }

        }

        public static void CreatData(string path,bool isNew=false)
        {
            if (File.Exists(path))
            {
                Debug.Log("File Has Existed!");
            }
            else
            {
                File.Create(path).Dispose();          

                Debug.Log("Success Create" + path);
            }

        }
        /// <summary>
        /// 获取Enum的Description内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetEnumDescription<T>(T obj)
        {
            var type = obj.GetType();
            FieldInfo field = type.GetField(Enum.GetName(type, obj));
            DescriptionAttribute descAttr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (descAttr == null)
            {
                return string.Empty;
            }

            return descAttr.Description;
        }

        public static T GetConfig<T>() where T : new()
        {
            var path = Application.dataPath + "/" + typeof(T).Name + ".json";
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


    }
}

