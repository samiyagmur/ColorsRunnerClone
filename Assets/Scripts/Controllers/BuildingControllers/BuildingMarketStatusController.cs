using System;
using Managers;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class BuildingMarketStatusController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion
        #region Serialized Variables
        
        [SerializeField] private TextMeshPro marketPriceText;
        [SerializeField] private BuildingManager manager;

        #endregion
    
        #region Private Variables


        #endregion
        

        #endregion
        

        public void SetRequiredAmountToText()
        {
            marketPriceText.text = $"{manager.BuildingsData.PayedAmount}/{manager.BuildingsData.BuildingMarketPrice}";
            
        }

        public void UpdatePayedAmountText(int payedAmount)
        {
            manager.BuildingsData.PayedAmount = payedAmount;
            SetRequiredAmountToText();
        }
        
    }
}
