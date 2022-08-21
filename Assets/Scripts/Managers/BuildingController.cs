using System;
using System.Collections.Generic;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class BuildingController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

         public int BuildingsAdressId;
         public BuildingType BuildingType;
         public int PayedAmounth;
         public int buildingMarketPrice;

        
        #endregion
        
        #region Public Variables
        

        #endregion

        #region Private Variables
        
        [SerializeField] private TextMeshProUGUI MarketPriceText;

        #endregion
        

        #endregion
        
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }

        private void UnsubscribeEvents()
        {
            
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

      
        
        
    }
}