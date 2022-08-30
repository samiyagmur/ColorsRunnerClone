using System;
using System.Threading.Tasks;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class BuildingScorePhysicsController : MonoBehaviour
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
                
                    if (buildingManager.BuildingsData.BuildingMarketPrice > buildingManager.BuildingsData.PayedAmount)
                    {
                        buildingManager.UpdatePayedAmount();

                    }
                    else
                    {

                        if (buildingManager.BuildingsData.idleLevelState == IdleLevelState.Uncompleted)
                        {
                            buildingManager.UpdateBuildingStatus(IdleLevelState.Completed);
                            buildingManager.Save(buildingManager.BuildingAddressID);
                        }
                    }
                
                } 
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _timer = 0f;
                buildingManager.Save(buildingManager.BuildingAddressID);
                
            }
        }
        
     
    }
}