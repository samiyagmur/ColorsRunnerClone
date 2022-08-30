using Controllers;
using Enums;
using Signals;
using System;
using Abstract;
using Datas.UnityObject;
using Datas.ValueObject;
using DG.Tweening;
using UnityEngine;


namespace Managers
{
    public class BuildingManager : MonoBehaviour,ISaveable
    {
        #region Self Variables

        #region Serialized Variables

        public BuildingsData BuildingsData;
        [SerializeField] private BuildingMarketStatusController buildingMarketStatusController;
        [SerializeField] private GameObject sideObject;
        [SerializeField] private BuildingMeshController buildingMeshController;
        [SerializeField] private BuildingPhysicsController buildingPhysicsController;
        [SerializeField] private BuildingScorePhysicsController buildingScorePhysicsController;

        #endregion

        #region Public Variables

        public int BuildingAddressID;

        #endregion

        #region Private Variables
        
        private int _IdleLevelID;
        
        #endregion

        #endregion
        
        private BuildingsData GetBuildingsData()
        {
            return Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_IdleLevelID].Buildings[BuildingAddressID]; 
        } 
        
        private void Awake()
        {   
          SetData();
      
        }

        private void SetData()
        {
            GetIdleLevelID();
            
            if (!ES3.FileExists($"IdleBuildingDataKey{BuildingAddressID}.es3"))
            {
                if (!ES3.KeyExists("IdleBuildingDataKey"))
                {   
                    Debug.Log("Key does not exist!");
                    BuildingsData = GetBuildingsData();
                    Save(BuildingAddressID);
                }
            }
            Debug.Log("Key Exist!");
            Load(BuildingAddressID);
            SetDataToControllers();
        }
        
        private void GetIdleLevelID()
        {
            _IdleLevelID = CoreGameSignals.Instance.onGetIdleLevelID.Invoke();
        }
        
        #region Event Subscription
        
        private void OnEnable()
        {
            SubscribeEvents();
           
        }

        private void SubscribeEvents()
        {
            
            CoreGameSignals.Instance.onApplicationPause += OnSave;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            CoreGameSignals.Instance.onNextLevel += OnSave;
            CoreGameSignals.Instance.onLevelInitialize += OnLoad;


        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onApplicationPause -= OnSave;
            CoreGameSignals.Instance.onApplicationQuit -= OnSave;
            CoreGameSignals.Instance.onNextLevel -= OnSave;
            CoreGameSignals.Instance.onLevelInitialize -= OnLoad;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
        
        private void SetDataToControllers() 
        {
            
            buildingMarketStatusController.UpdatePayedAmountText(BuildingsData.PayedAmount);
            buildingMeshController.Saturation = BuildingsData.Saturation;
            UpdateSaturation();
            UpdateBuildingStatus(BuildingsData.idleLevelState);

        }

        #region Save-Load
        
        private void OnSave()
        {
            Save(BuildingAddressID);
            SetDataToControllers();
        }

        private void OnLoad()
        {
            Load(BuildingAddressID);
            SetDataToControllers();
        }
        
        public void Save(int uniqueId)
        {   

            BuildingsData  = new BuildingsData(BuildingsData.IsDepended,
                BuildingsData.SideObject,
                BuildingAddressID,
                BuildingsData.BuildingMarketPrice,
                BuildingsData.PayedAmount,
                BuildingsData.Saturation,
                BuildingsData.idleLevelState);
            
            SaveLoadSignals.Instance.onSaveBuildingsData.Invoke(BuildingsData,uniqueId);
        }

        public void Load(int uniqueId)
        { 
            BuildingsData _buildingsData = SaveLoadSignals.Instance.onLoadBuildingsData.Invoke(BuildingsData.Key, uniqueId);
            
            BuildingsData.Saturation = _buildingsData.Saturation;
            BuildingsData.PayedAmount = _buildingsData.PayedAmount;
            BuildingsData.idleLevelState = _buildingsData.idleLevelState;
            BuildingsData.BuildingMarketPrice = _buildingsData.BuildingMarketPrice;
            BuildingsData.IsDepended = _buildingsData.IsDepended;
        }

        #endregion
        

        #region  UpdateControllers
        
        public void UpdatePayedAmount()
        {   
            BuildingsData.PayedAmount++;
            buildingMarketStatusController.UpdatePayedAmountText(BuildingsData.PayedAmount);
            UpdateSaturation();
            
        }

        private void UpdateSaturation()
        {   
            buildingMeshController.CalculateSaturation();
        }


        public void UpdateBuildingStatus(IdleLevelState _idleLevelState)
        {
            BuildingsData.idleLevelState = _idleLevelState;
            if (_idleLevelState == IdleLevelState.Completed)
            {  
                buildingPhysicsController.gameObject.SetActive(false);
                if (!BuildingsData.IsDepended)
                {
                    BuildingSignals.Instance.onBuildingsCompleted.Invoke(BuildingsData.BuildingAdressId);
                }
            }
        }
        

        #endregion
       
        
        
    
    }
}