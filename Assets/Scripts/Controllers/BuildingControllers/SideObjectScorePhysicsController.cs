using Enums;
using Managers;
using UnityEngine;

namespace Controllers.BuildingControllers
{
    public class SideObjectScorePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private BuildingManager buildingManager;

        #endregion

        #region Private Variables

        private float _timer = 0f;

        #endregion

        #endregion

        private void OnTriggerStay(Collider other)   //Where is the tag
        {
            if (other.CompareTag("Player"))
            {
                _timer -= Time.fixedDeltaTime ;
            
                if (_timer <= 0)
                {
                    _timer = 0.2f;
                
                    if (buildingManager.BuildingsData.SideObject.BuildingMarketPrice > buildingManager.BuildingsData.SideObject.PayedAmount)
                    {
                        buildingManager.UpdateSidePayedAmount();

                    }
                    else
                    {

                        if (buildingManager.BuildingsData.SideObject.ıdleLevelState == IdleLevelState.Uncompleted)
                        {
                            buildingManager.UpdateSideBuildingStatus(IdleLevelState.Completed);
                            buildingManager.CheckSideBuildingsScoreStatus(IdleLevelState.Completed);
                        }
                    }
                
                } 
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {   
                if (buildingManager.BuildingsData.SideObject.BuildingMarketPrice == buildingManager.BuildingsData.SideObject.PayedAmount)
                {
                 
                    buildingManager.UpdateSideBuildingStatus(IdleLevelState.Completed);
                    buildingManager.CheckSideBuildingsScoreStatus(IdleLevelState.Completed);
                }
                _timer = 0f;
                buildingManager.Save(buildingManager.BuildingAddressID);
                
            }
        }
    }
}