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

        public BuildingsData buildingsData;
        [SerializeField] private BuildingMarketStatusController buildingMarketStatusController;
        [SerializeField] private GameObject SideObject;
        [SerializeField] private SideObject sideObjectData;
        [SerializeField] private BuildingMeshController buildingMeshController;
        [SerializeField] private BuildingPhysicsController buildingPhysicsController;
        [SerializeField] private BuildingScorePhysicsController buildingScorePhysicsController;

        #endregion

        #region Public Variables


        public int BuildingAddressID;
        public int _idleLevelId;
        

        #endregion

        #region Private Variables
        
        
        #endregion
        

        #endregion
        
        private BuildingsData GetBuildingsData()
        {
            return Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_idleLevelId].Buildings[BuildingAddressID]; 
        } 
        
        private void Awake()
        {   
            GetIdleLevelID();
            
            if (!ES3.FileExists($"IdleBuildingDataKey{BuildingAddressID}.es3"))
            {
                if (!ES3.KeyExists("IdleBuildingDataKey"))
                {   
                    Debug.Log("Key does not exist!");
                    buildingsData = GetBuildingsData();
                    Save(BuildingAddressID);
                    
                }
            }
          
            Debug.Log("Key Exist!");
            Load(BuildingAddressID);
            SetDataToControllers();
            
            
            
        }
        
        private void GetIdleLevelID()
        {
            _idleLevelId = CoreGameSignals.Instance.onGetIdleLevelID.Invoke();
        }
       
        
        #region Event Subscription
        
        private void OnEnable()
        {
            SubscribeEvents();
           
        }

        private void SubscribeEvents()
        {
            //BuildingSignals.Instance.onDataReadyToUse += OnSetDataToControllers;
            CoreGameSignals.Instance.onApplicationPause += OnSave;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            CoreGameSignals.Instance.onNextLevel += OnSave;
            CoreGameSignals.Instance.onLevelInitialize += OnLoad;

        }

        private void UnsubscribeEvents()
        {
            //BuildingSignals.Instance.onDataReadyToUse -= OnSetDataToControllers; // Invoke atiliyor ama bu arkadaslar dinlemiyor,ilginc
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

            buildingsData  = new BuildingsData(buildingsData.IsDepended,
                buildingsData.SideObject,
                BuildingAddressID,
                buildingsData.BuildingMarketPrice,
                buildingsData.PayedAmount,
                buildingsData.Saturation,
                buildingsData.idleLevelState);
            
            SaveLoadSignals.Instance.onSaveIdleData.Invoke(buildingsData,uniqueId);
        }

        public void Load(int uniqueId)
        { 
            BuildingsData _buildingsData = SaveLoadSignals.Instance.onLoadBuildingsData.Invoke(buildingsData.Key, uniqueId);
            
           buildingsData.Saturation = _buildingsData.Saturation;
           buildingsData.PayedAmount = _buildingsData.PayedAmount;
           buildingsData.idleLevelState = _buildingsData.idleLevelState;
           buildingsData.BuildingMarketPrice = _buildingsData.BuildingMarketPrice;
           buildingsData.IsDepended = _buildingsData.IsDepended;
        }
        
        private void SetDataToControllers() 
        {
            
            buildingMarketStatusController.UpdatePayedAmountText(buildingsData.PayedAmount);
            buildingMeshController.Saturation = buildingsData.Saturation;
            UpdateSaturation();

        }

        public void UpdatePayedAmount()
        {   
            buildingsData.PayedAmount++;
            buildingMarketStatusController.UpdatePayedAmountText(buildingsData.PayedAmount);
            UpdateSaturation();
            
        }

        private void UpdateSaturation()
        {   
            buildingMeshController.CalculateSaturation();
        }

        public void UpdateBuildingStatus(IdleLevelState idleLevelState)
        {
            buildingsData.idleLevelState = idleLevelState;
            BuildingSignals.Instance.onBuildingsCompleted.Invoke(buildingsData.BuildingAdressId);
        }

        public void OpenSideObject()
        {
            SideObject.SetActive(true);
            buildingMarketStatusController.gameObject.SetActive(false);
        }
    
    }
}