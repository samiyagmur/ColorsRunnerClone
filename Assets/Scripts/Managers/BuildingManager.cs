using System;
using System.Collections.Generic;
using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using TMPro;
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
        
        public int BuildingsAdressId;
        
        public int PayedAmount;
        
        public int BuildingMarketPrice;
        
        public float Saturation;

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
            buildingMarketStatusController.PayedAmount = PayedAmount;
            
        }

        private void SetDataToBuildingMeshController()
        {
            buildingMeshController.Saturation = Saturation;
        }

        public void UpdatePayedAmount()
        {
            buildingMarketStatusController.UpdatePayedAmountText();
        }

        public void UpdateSaturation()
        {
            buildingMeshController.UpdateSaturation();
        }
        
        //Data control needs
    }
}