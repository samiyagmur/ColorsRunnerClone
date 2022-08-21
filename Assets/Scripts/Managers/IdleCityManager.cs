using System;
using System.Collections;
using System.Collections.Generic;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class IdleCityManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("BuildingsData")] public IdleLevelData IdleLevelData;

        public List<GameObject> Buildings = new List<GameObject>();

        public List<BuildingController> BuildingControllers = new List<BuildingController>();

        public List<GameObject> PublicPlaces = new List<GameObject>();
        
        public List<Transform> BuildingsTransforms = new List<Transform>();
        
        public List<Transform> PublicPlacesTransforms = new List<Transform>();

        public List<Transform> SideObjectTransforms = new List<Transform>();

        #endregion

        #region Private Variables

        private int index;
        private int _idleLevelId;

        #endregion

        #region Serialized Variables

        

        #endregion

        #endregion

 
       private IdleLevelData OnGetCityData() => Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_idleLevelId];

       private void GetIdleLevelData()
       {
           _idleLevelId = CoreGameSignals.Instance.onGetIdleLevelID.Invoke();
       }

       private void Start()
       {
           GetIdleLevelData();
           IdleLevelData = OnGetCityData();
           Debug.Log(IdleLevelData);
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
        
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onApplicationPause -= OnSaveCityData;
            CoreGameSignals.Instance.onApplicationQuit -= OnSaveCityData;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SaveCityData(IdleLevelData ıdleLevelData)
        {
            //SaveLoadSignals.Instance.onSaveIdleLevelData?.Invoke(ıdleLevelData);
        }

        private void OnSaveCityData()
        {
            //SaveCityData(ıdleLevelData);
        }

        private void SetBuildings()
        {
            
            for (int i = 0; i < IdleLevelData.Buildings.Count; i++)
            {   
                if (IdleLevelData.Buildings[i].isDepended is false)
                {
                    BuildingControllers[i].BuildingsAdressId = IdleLevelData.Buildings[i].BuildingAdressId;
                    BuildingControllers[i].BuildingType = IdleLevelData.Buildings[i].BuildingType ;
                    BuildingControllers[i].PayedAmounth = IdleLevelData.Buildings[i].PayedAmount;
                    BuildingControllers[i].buildingMarketPrice = IdleLevelData.Buildings[i].BuildingMarketPrice;
                }
                else if(IdleLevelData.Buildings[i].isDepended && IdleLevelData.Buildings[i].ıdleLevelState == IdleLevelState.Completed)
                {
                    // IdleLevelData.Buildings[i].SideObject.BuildingType
                    // IdleLevelData.Buildings[i].SideObject.PayedAmount
                    // IdleLevelData.Buildings[i].SideObject.BuildingMarketPrice
                    // IdleLevelData.Buildings[i].SideObject.RequiredBuildingAdressId
                }
                
            }
        }

        private void SetPublicPlaces()
        {
            for (int i = 0; i < IdleLevelData.PublicPlacesDatas.Count; i++)
            {
                // IdleLevelData.PublicPlacesDatas[i].RequiredBuilds
                // IdleLevelData.PublicPlacesDatas[i].ıdleLevelState     
                // IdleLevelData.PublicPlacesDatas[i].PayedAmound
                // IdleLevelData.PublicPlacesDatas[i].PublicPlacesType
                    
            }
        }

       
        
    }
}