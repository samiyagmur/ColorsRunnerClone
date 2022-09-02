using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.BuildingControllers
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
                        ParticalSignals.Instance.onParticleBurst?.Invoke(transform.position);
                    }
                    else
                    {
                        if (buildingManager.BuildingsData.idleLevelState == IdleLevelState.Uncompleted)
                        {   
                            buildingManager.OpenSideObject();
                            ParticalSignals.Instance.onParticleStop?.Invoke();
                            //score burada durcak.
                            buildingManager.UpdateBuildingStatus(IdleLevelState.Completed);
                            buildingManager.CheckBuildingsScoreStatus(IdleLevelState.Completed);
                        }
                    }
                
                } 
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {   
                if (buildingManager.BuildingsData.BuildingMarketPrice == buildingManager.BuildingsData.PayedAmount)
                {
                    buildingManager.OpenSideObject();
                    buildingManager.UpdateBuildingStatus(IdleLevelState.Completed);
                    buildingManager.CheckBuildingsScoreStatus(IdleLevelState.Completed);
                }
                _timer = 0f;
                
                buildingManager.Save(buildingManager.BuildingAddressID);

            }
        }
        
     
    }
}