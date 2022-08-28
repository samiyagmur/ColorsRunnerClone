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

        [SerializeField] private BuildingsData buildingsData;
        [SerializeField] private BuildingMarketStatusController buildingMarketStatusController;
        [SerializeField] private GameObject SideObject;
        [SerializeField] private SideObject sideObjectData;
        [SerializeField] private BuildingMeshController buildingMeshController;
        [SerializeField] private BuildingPhysicsController buildingPhysicsController;
        [SerializeField] private BuildingScorePhysicsController buildingScorePhysicsController;

        #endregion

        #region Public Variables
        
        public IdleLevelState IdleLevelState;
        
        public int BuildingsAdressId;
        
        public int PayedAmount;
        
        public int BuildingMarketPrice;
        
        public float Saturation;

        public bool isDepended;
        

        #endregion

        #region Private Variables
        
        
        #endregion
        

        #endregion
        
        private void Awake()
        {
            if (!ES3.FileExists())
            {
                buildingsData = GetBuildingsData();
                Save(BuildingsAdressId);
            }
            Debug.Log(BuildingsAdressId);
            
        }
        private BuildingsData GetBuildingsData()
        {
            return Resources.Load<CD_IdleLevel>("Data/CD_IdleLevelData").IdleLevelList[0].Buildings[BuildingsAdressId]; 
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
            CoreGameSignals.Instance.onNextLevel += OnLoad;
            CoreGameSignals.Instance.onLevelInitialize += OnLoad;

        }

        private void UnsubscribeEvents()
        {
            //BuildingSignals.Instance.onDataReadyToUse -= OnSetDataToControllers; // Invoke atiliyor ama bu arkadaslar dinlemiyor,ilginc
            CoreGameSignals.Instance.onApplicationPause -= OnSave;
            CoreGameSignals.Instance.onApplicationQuit -= OnSave;
            CoreGameSignals.Instance.onNextLevel -= OnSave;
            CoreGameSignals.Instance.onNextLevel -= OnLoad;
            CoreGameSignals.Instance.onLevelInitialize -= OnLoad;

        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSave()
        {
            Save(BuildingsAdressId);
        }

        private void OnLoad()
        {
            Load(BuildingsAdressId);
        }
        public void Save(int uniqueId)
        {
            buildingsData  = new BuildingsData(isDepended,sideObjectData,BuildingsAdressId,BuildingMarketPrice,PayedAmount,Saturation,IdleLevelState);
            SaveLoadSignals.Instance.onSaveIdleData.Invoke(buildingsData,uniqueId);
        }

        public void Load(int uniqueId)
        {
            buildingsData = SaveLoadSignals.Instance.onLoadBuildingsData.Invoke(buildingsData.Key, uniqueId);
            
            IdleLevelState = buildingsData.idleLevelState;
            BuildingsAdressId = buildingsData.BuildingAdressId;
            BuildingMarketPrice = buildingsData.BuildingMarketPrice;
            PayedAmount = buildingsData.PayedAmount;
            Saturation = buildingsData.Saturation;
            isDepended = buildingsData.IsDepended;
            Debug.Log(PayedAmount);
            SetDataToControllers();
        }
        
        private void SetDataToControllers() 
        {
            buildingMarketStatusController.MarketPrice = BuildingMarketPrice;
            buildingMarketStatusController.PayedAmount = PayedAmount;
            buildingMarketStatusController.UpdatePayedAmountText(PayedAmount);
            buildingMeshController.Saturation = Saturation;
            UpdateSaturation();

        }

        public void UpdatePayedAmount()
        {   
            PayedAmount++;
            buildingMarketStatusController.UpdatePayedAmountText(PayedAmount);
            UpdateSaturation();
            
        }

        private void UpdateSaturation()
        {   
             Saturation = buildingMeshController.CalculateSaturation();
        }

        public void UpdateBuildingStatus(IdleLevelState idleLevelState)
        {
            IdleLevelState = idleLevelState;
            BuildingSignals.Instance.onBuildingsCompleted.Invoke(BuildingsAdressId);
        }

        public void OpenSideObject()
        {
            SideObject.SetActive(true);
            buildingMarketStatusController.gameObject.SetActive(false);
        }
    
    }
}