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
        
        [SerializeField] private TextMeshPro MarketPriceText;
        [SerializeField] private BuildingManager buildingManager;
        #endregion
    
        #region Private Variables
        
    

        #endregion
        

        #endregion
    
        private void Awake()
        {
        
        }

        private void OnEnable()
        {
        
        }

        private void Start()
        {
            SetRequiredAmountToText();
        }

        private void SetRequiredAmountToText()
        {
            MarketPriceText.text = $"{buildingManager.PayedAmounth}/{buildingManager.BuildingMarketPrice}";
        }

        private void UpdateRequiredTextCount()
        {
        
        }

        public void ControlCompletionState()
        {
       
        }
    }
}
