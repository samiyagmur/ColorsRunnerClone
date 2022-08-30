using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abstract;
using Datas.UnityObject;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class IdleCityManager : MonoBehaviour,ISaveable
    {
        #region Self Variables

        #region Public Variables

        [Header("BuildingsData")] public IdleLevelData IdleLevelData;

        public List<BuildingManager> BuildingManagers = new List<BuildingManager>();

        public IdleLevelState IdleLevelState;
        

        #endregion

        #region Private Variables
        
        private int _idleLevelId;

        #endregion
        

        #endregion

 
       private IdleLevelData GetIdleData() => Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_idleLevelId];

       private void GetIdleLevelData()
       {
           _idleLevelId = CoreGameSignals.Instance.onGetIdleLevelID.Invoke();
       }
       

       private void Start()
       {
           SetData();
       }

       private void SetData()
       {
           GetIdleLevelData();
           if (!ES3.FileExists($"IdleLevelDataKey{_idleLevelId}.es3"))
           {
               if (!ES3.KeyExists("IdleBuildingDataKey"))
               {
                   GetIdleLevelData();
                   IdleLevelData = GetIdleData();
                   Save(_idleLevelId);
               }
           }
           Load(_idleLevelId);
       }
       
       #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BuildingSignals.Instance.onBuildingsCompleted += OnIncreaseCompletedCount;
            CoreGameSignals.Instance.onApplicationPause += OnSave;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            CoreGameSignals.Instance.onLevelInitialize += OnLoad;
        }

        private void UnsubscribeEvents()
        {
            BuildingSignals.Instance.onBuildingsCompleted -= OnIncreaseCompletedCount;
            CoreGameSignals.Instance.onApplicationPause -= OnSave;
            CoreGameSignals.Instance.onApplicationQuit -= OnSave;
            CoreGameSignals.Instance.onLevelInitialize -= OnLoad;

        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Save-Load

        private void OnSave()
        {
            Save(_idleLevelId);
        }

        private void OnLoad()
        {
            Load(_idleLevelId);
        }
        public void Save(int uniqueId)
        {
            IdleLevelData = new IdleLevelData(IdleLevelData.IdleLevelState,IdleLevelData.CompletedBuildingsCount);
            SaveLoadSignals.Instance.onSaveIdleData.Invoke(IdleLevelData,uniqueId);
        }

        public void Load(int uniqueId)
        {
            
            IdleLevelData _IdleLevelData = SaveLoadSignals.Instance.onLoadIdleData.Invoke(IdleLevelData.IdleLevelKey, uniqueId);

            IdleLevelData.IdleLevelState = _IdleLevelData.IdleLevelState;
            IdleLevelData.CompletedBuildingsCount = _IdleLevelData.CompletedBuildingsCount;

        }

        #endregion
      
     
        private void OnIncreaseCompletedCount(int addressId)
        {
            IdleLevelData.CompletedBuildingsCount++;
            SetIdleLevelStatus();
            Save(_idleLevelId);
        }
        
        private void SetIdleLevelStatus()
        {
            if (IdleLevelData.CompletedBuildingsCount == BuildingManagers.Count)
            {
                IdleLevelData.IdleLevelState = IdleLevelState.Completed;
                Save(_idleLevelId);
                CoreGameSignals.Instance.onIdleLevelChange.Invoke();
                
            }
   
        }
        
        
        

      
    }
}