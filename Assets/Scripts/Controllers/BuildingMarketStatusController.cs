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
            marketPriceText.text = $"{manager.buildingsData.PayedAmount}/{manager.buildingsData.BuildingMarketPrice}";
            
        }

        public void UpdatePayedAmountText(int payedAmount)
        {
            manager.buildingsData.PayedAmount = payedAmount;
            SetRequiredAmountToText();
        }
        
    }
}
