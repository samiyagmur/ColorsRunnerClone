using Managers;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class BuildingMarketStatusController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public int PayedAmount;
        public int MarketPrice;

        #endregion
        #region Serialized Variables
        
        [SerializeField] private TextMeshPro MarketPriceText;
        [SerializeField] private BuildingManager buildingManager;
        
        #endregion
    
        #region Private Variables
        
    

        #endregion
        

        #endregion
        private void Start()
        {
            SetRequiredAmountToText();
        }
        
        
        private void SetRequiredAmountToText()
        {
            MarketPriceText.text = $"{buildingManager.PayedAmount}/{buildingManager.BuildingMarketPrice}";
        }

        public void UpdatePayedAmountText()
        {
             //Set edicez
        }

        public void ControlCompletionState()
        {
       
        }
        
    }
}
