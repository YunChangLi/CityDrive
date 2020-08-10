using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataSync;
using UnityEngine;



namespace LabData
{
    public enum SaveType
    {
        Json,
        Csv
    }

    public class LabDataManager : MonoSingleton<LabDataManager>
    {
        [SerializeField] private SaveType _saveType = SaveType.Json;
        [SerializeField] private bool _sendToServer = false;

        private bool _isClientRunning = false;
        private bool _isClientInit = false;
        private static DataSyncClient _client;
        public LabDataScope Scope { get; private set; }
        private string _userId;
        private SimpleApplicationLifecycle _applicationLifecycle;
        private string labDataSavePath => Application.dataPath + "/TestData";
        private readonly List<DataWriter> _dataWriters = new List<DataWriter>();


        private string _localSaveDataTimeLayout;

        /// <summary>
        /// 数据采集,传入数据,频率,是否循环采集
        /// </summary>
        /// <param name="data"></param>
        /// <param name="loop"></param>
        /// <param name="frequency"></param>
        public void DataCollect(IData data, bool loop = true, int frequency = 200)
        {
            if (!_isClientInit)
            {
                Debug.LogError("LabData未初始化");
                return;
            }
               
           
            //StartUpload();
            var basePathStr = Application.dataPath + "/Output";
            LabTools.CreatFolder(basePathStr);
            var userStr = _userId.PadLeft(2, '0');
            basePathStr = string.Join("_", basePathStr + "/" + DateTime.Now.ToString(_localSaveDataTimeLayout), userStr);
            basePathStr = LabTools.CreatFolder(basePathStr);

            if (_saveType == SaveType.Csv)
            {
               
                    string dataPath = string.Join("_", basePathStr + "/" + DateTime.Now.ToString(_localSaveDataTimeLayout), userStr, data.SaveData.DataCodeName, data.SaveData.LabDataBase.Invoke() + "." + _saveType.ToString());
                    LabTools.CreatData(dataPath);
                    DataWriter dw = new DataWriter(dataPath, data.SaveData.LabDataBase.Invoke, _saveType);
                    dw.WriteCsvTitle();
                    dw.Dispose();
               

            }
            if (loop)
            {
                
                    string dataPath = string.Join("_", basePathStr + "/" + DateTime.Now.ToString(_localSaveDataTimeLayout), userStr, data.SaveData.DataCodeName, data.SaveData.LabDataBase.Invoke() + "." + _saveType.ToString());
                    LabTools.CreatData(dataPath);
                    DataWriter dw = new DataWriter(dataPath,
                        () =>
                        {
                            if (_sendToServer)
                            {
                                Scope.Send(data.SaveData.LabDataBase.Invoke());
                            }
                            return data.SaveData.LabDataBase.Invoke();
                        }, _saveType,frequency);
                    dw.WriteDataAsyncFrequency();
                    _dataWriters.Add(dw);
             

            }
            else
            {
               string dataPath = string.Join("_", basePathStr + "/" + DateTime.Now.ToString(_localSaveDataTimeLayout), userStr, data.SaveData.DataCodeName, data.SaveData.LabDataBase.Invoke() + "." + _saveType.ToString());
                    LabTools.CreatData(dataPath);
                    DataWriter dw = new DataWriter(dataPath,()=>
                    {
                        if (_sendToServer)
                        {
                            Scope.Send(data.SaveData.LabDataBase.Invoke());
                        }
                        return data.SaveData.LabDataBase.Invoke();
                    }, _saveType);
                    dw.WriteOnce();
                    dw.Dispose();
              
            }

        }

        /// <summary>
        /// 传入UserID初始化LabDataCollect
        /// </summary>
        /// <param name="userId"></param>
        public void LabDataCollectInit(string userId)
        {
            _localSaveDataTimeLayout = LabTools.GetConfig<LabDataConfig>().LocalSaveDataTimeLayout;
            if (_isClientInit)
            {
                return;
            }
            _userId = userId;
            //StartAutoDataCollect();
            var options = new DataSyncClientOptions()
            {
                EndpointAddress = "http://localhost:4000/api/data",
                ProjectId = LabTools.GetConfig<LabDataConfig>().ProjectId,
                LogFilePath = labDataSavePath + "/ log.txt"
            };

            //Docker
            options.EndpointAddress = "http://localhost/api/data";

            //server
            _sendToServer = LabTools.GetConfig<LabDataConfig>().SendToServer;
            options.EndpointAddress = LabTools.GetConfig<LabDataConfig>().ServerPath;
           

            if (!Directory.Exists("TestStore"))
            {
                Directory.CreateDirectory("TestStore");
            }
            _applicationLifecycle = new SimpleApplicationLifecycle();


            _client = new DataSyncClient(new UnityApplicationFolderProvider(labDataSavePath + "/TestStore"),
                _applicationLifecycle, options, () => _userId);

            _client.Init();

            _isClientInit = true;

            StartUpload();
        }


        /// <summary>
        /// 释放LabData
        /// </summary>
        public void LabDataDispose()
        {
            Debug.Log("LabDataDispose");
            StopUpload();
            _isClientInit = false;
            _dataWriters?.ForEach(p => p.Dispose());
        }

        void StartUpload()
        {
            if (_isClientRunning)
            {
                return;
            }
            Debug.Log("开始");
            _applicationLifecycle.OnStarted(EventArgs.Empty);
            Scope = _client.CreateNewScope();
            Scope.StartScope();
            _isClientRunning = true;
        }

        void StopUpload()
        {
            if (!_isClientRunning)
            {
                return;
            }
            Debug.Log("停止");
            Scope.StopScope();
            Scope.Dispose();

            _applicationLifecycle.OnStopping(ApplicationStoppingEventArgs.Empty);
            _isClientRunning = false;
        }

        protected override void OnApplicationQuit()
        {
            LabDataDispose();
            base.OnApplicationQuit();
        }

        void OnDestroy()
        {
            LabDataDispose();
        }


        

    }
}

