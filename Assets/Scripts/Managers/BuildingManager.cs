using Controllers;
using Enums;
using Signals;
using System;
using UnityEngine;


namespace Managers
{
    public class BuildingManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BuildingMarketStatusController buildingMarketStatusController;
        [SerializeField] private BuildingMeshController buildingMeshController;
        [SerializeField] private BuildingPhysicsController buildingPhysicsController;
        [SerializeField] private BuildingScorePhysicsController buildingScorePhysicsController;

        #endregion

        #region Public Variables

        public IdleLevelState IdleLevelState;
        
        public int BuildingsAdressId;
        
        public int PayedAmount;
        
        public int BuildingMarketPrice;
        
        public float Saturation;  //struct

        #endregion

        #region Private Variables
        
        
        #endregion
        

        #endregion

        #region Event Subscription
        
        private void OnEnable()
        {
          
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            BuildingSignals.Instance.onDataReadyToUse += OnSetDataToControllers;

        }

        private void UnsubscribeEvents()
        {
            BuildingSignals.Instance.onDataReadyToUse -= OnSetDataToControllers;

        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSetDataToControllers()
        {
            SetDataToBuildingMeshController();
            SetDataToBuildingMarketStatusController();
            
        }
        private void SetDataToBuildingMarketStatusController() 
        {
            buildingMarketStatusController.MarketPrice = BuildingMarketPrice;
            buildingMarketStatusController.UpdatePayedAmountText(PayedAmount);
            buildingMarketStatusController.PayedAmount = PayedAmount;
             UpdateSaturation();

        }

        private void SetDataToBuildingMeshController()
        {
            buildingMeshController.Saturation = Saturation;
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

        //Data control needs
    }
}