using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class IdleCityManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("BuildingsData")] public IdleLevelData IdleLevelData;

        public List<GameObject> Buildings = new List<GameObject>();

        public List<BuildingManager> BuildingManager = new List<BuildingManager>();

        public List<Transform> BuildingsTransforms = new List<Transform>();
        

        #endregion

        #region Private Variables

        private int _index;
        private int _idleLevelId;

        #endregion

        #region Serialized Variables
        

        #endregion

        #endregion

 
       private IdleLevelData OnGetCityData() => Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_idleLevelId]; //SaveManager çeksin bize passlasin,

       private void GetIdleLevelData()
       {
           _idleLevelId = CoreGameSignals.Instance.onGetIdleLevelID.Invoke();
       }

       private void Start()
       {
           GetIdleLevelData();
           
           
           if (!ES3.FileExists("IdleLevelProgress/IdleLevelProgressData.es3"))
           {
               IdleLevelData = OnGetCityData();

               SaveCityData(IdleLevelData); 
           }
           
           IdleLevelData = OnGetCityData();
           
           LoadCityData(IdleLevelData);
           

           SetDataToBuildingManagers();

       }

       #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onApplicationPause += OnSaveCityData;
            CoreGameSignals.Instance.onApplicationQuit += OnSaveCityData;
            BuildingSignals.Instance.onBuildingsCompleted += OnSetBuildingsStatus;
            CoreGameSignals.Instance.onLevelInitialize += OnLoadCityData;
            CoreGameSignals.Instance.onApplicationQuit += OnGetBuildingsDataFromBuildingManagers;
            CoreGameSignals.Instance.onReset += OnGetBuildingsDataFromBuildingManagers;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onApplicationPause -= OnSaveCityData;
            CoreGameSignals.Instance.onApplicationQuit -= OnSaveCityData;
            BuildingSignals.Instance.onBuildingsCompleted -= OnSetBuildingsStatus;
            CoreGameSignals.Instance.onLevelInitialize -= OnLoadCityData;
            CoreGameSignals.Instance.onApplicationQuit -= OnGetBuildingsDataFromBuildingManagers;
            CoreGameSignals.Instance.onReset -= OnGetBuildingsDataFromBuildingManagers;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
 
        private void SaveCityData(IdleLevelData ıdleLevelData)
        {
            SaveLoadSignals.Instance.onSaveIdleLevelProgressData?.Invoke(SaveStates.IdleLevelProgress,ıdleLevelData);
        }
        

        private IdleLevelData LoadCityData(IdleLevelData idleLevelData)
        {
            return SaveLoadSignals.Instance.onLoadIdleLevelProgressData.Invoke(SaveStates.IdleLevelProgress,idleLevelData);
        }
        
        private void OnSaveCityData()
        {
            SaveCityData(IdleLevelData);
        }

        private void OnLoadCityData()
        {
            LoadCityData(IdleLevelData);
        }

        private void SetDataToBuildingManagers()
        {
            for (int i = 0; i < Buildings.Count; i++)
            {
                IdleLevelData.Buildings[i].BuildingAdressId = i;
                
                BuildingManager[i].BuildingsAdressId = i;

                BuildingManager[i].BuildingMarketPrice= IdleLevelData.Buildings[i].BuildingMarketPrice;
                
                BuildingManager[i].PayedAmount  = IdleLevelData.Buildings[i].PayedAmount;
                
                BuildingManager[i].Saturation = IdleLevelData.Buildings[i].Saturation;

                if (IdleLevelData.Buildings[i].isDepended &&
                    IdleLevelData.Buildings[i].ıdleLevelState != IdleLevelState.Completed)
                {
                    IdleLevelData.Buildings[i].SideObject.BuildingAdressId = i;
                    
                    BuildingManager[i].BuildingsAdressId = i;  
                    
                    BuildingManager[i].PayedAmount = IdleLevelData.Buildings[i].SideObject.PayedAmount;
                    
                    BuildingManager[i].BuildingMarketPrice = IdleLevelData.Buildings[i].SideObject.BuildingMarketPrice;
                    
                    BuildingManager[i].Saturation= IdleLevelData.Buildings[i].SideObject.Saturation;
                    
                    OnSetSideObjects(i);
                }
                
                
            }  
            
            BuildingsDatasAreSync();
      
        }
        private void BuildingsDatasAreSync()
        {
            BuildingSignals.Instance.onDataReadyToUse?.Invoke();
        }
        private void OnGetBuildingsDataFromBuildingManagers()
        {
            for (int i = 0; i < BuildingManager.Count; i++)
            {
                IdleLevelData.Buildings[i].ıdleLevelState = BuildingManager[i].IdleLevelState;
                IdleLevelData.Buildings[i].PayedAmount = BuildingManager[i].PayedAmount;
                IdleLevelData.Buildings[i].Saturation = BuildingManager[i].Saturation;

                if (IdleLevelData.Buildings[i].isDepended &&
                    IdleLevelData.Buildings[i].ıdleLevelState == IdleLevelState.Completed)
                {
                    IdleLevelData.Buildings[i].ıdleLevelState = BuildingManager[i].IdleLevelState;
                    IdleLevelData.Buildings[i].SideObject.PayedAmount = BuildingManager[i].PayedAmount;
                    IdleLevelData.Buildings[i].SideObject.Saturation = BuildingManager[i].Saturation;
                    Debug.Log(IdleLevelData.Buildings[i].SideObject.PayedAmount);
                }
                
            }
            
            SaveCityData(IdleLevelData);
          
        }
        private void OnSetBuildingsStatus(int addressId)
        {
            IdleLevelData.Buildings[addressId].ıdleLevelState = IdleLevelState.Completed;
        }

        private void OnSetSideObjects(int addressId)
        {
            if (IdleLevelData.Buildings[addressId].isDepended && IdleLevelData.Buildings[addressId].ıdleLevelState == IdleLevelState.Completed)
            {
               BuildingManager[addressId].OpenSideObject();
            }
        }
    
    }
}