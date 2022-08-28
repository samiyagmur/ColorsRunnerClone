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
    public class IdleCityManager : MonoBehaviour
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

        #region Serialized Variables
        

        #endregion

        #endregion

 
       private IdleLevelData OnGetCityData() => Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_idleLevelId];

       private void GetIdleLevelData()
       {
           _idleLevelId = CoreGameSignals.Instance.onGetIdleLevelID.Invoke();
       }

       private void Awake()
       {
           GetIdleLevelData();

           IdleLevelData = OnGetCityData();

           //SetDataToBuildingManagers();

       }

       #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BuildingSignals.Instance.onBuildingsCompleted += OnSetBuildingsStatus;
        }

        private void UnsubscribeEvents()
        {
            BuildingSignals.Instance.onBuildingsCompleted -= OnSetBuildingsStatus;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void SetDataToBuildingManagers()
        {
            for (int i = 0; i <BuildingManagers.Count ; i++)
            {
                IdleLevelData.Buildings[i].BuildingAdressId = i;
                
                BuildingManagers[i].BuildingsAdressId = i;

                BuildingManagers[i].isDepended = IdleLevelData.Buildings[i].IsDepended;

                BuildingManagers[i].Saturation = IdleLevelData.Buildings[i].Saturation;

                BuildingManagers[i].PayedAmount = IdleLevelData.Buildings[i].PayedAmount;

                BuildingManagers[i].BuildingMarketPrice = IdleLevelData.Buildings[i].BuildingMarketPrice;

                BuildingManagers[i].IdleLevelState = IdleLevelData.Buildings[i].idleLevelState;
                
            }  
            
            DOVirtual.DelayedCall(.1f, ()=>{ BuildingsDatasAreSync();});

        }
        private void BuildingsDatasAreSync()
        {
            BuildingSignals.Instance.onDataReadyToUse?.Invoke();
        
        }
     
        private void OnSetBuildingsStatus(int addressId)
        {
            IdleLevelData.Buildings[addressId].idleLevelState = IdleLevelState.Completed;
        }

        private void OnSetSideObjects(int addressId)
        {
            if (IdleLevelData.Buildings[addressId].IsDepended && IdleLevelData.Buildings[addressId].idleLevelState == IdleLevelState.Completed)
            {
               BuildingManagers[addressId].OpenSideObject();
            }
        }
        
    }
}