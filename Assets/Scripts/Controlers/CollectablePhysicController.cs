using System.Collections;
using UnityEngine;
using Managers;
using Signals;

namespace Controlers
{
    public class CollectablePhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField]
        private CollectableManager collectableManager;
        

        #endregion

        #endregion
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && CompareTag("Collected"))
            {
                if (collectableManager.CollectableColorType == other.transform.parent.GetComponent<CollectableManager>().CollectableColorType)
                {
                    collectableManager.OnIcreaseStack();
                    other.tag = "Collected";
                }
                else
                {
                    collectableManager.OnDecreaseStack();
                    other.gameObject.SetActive(false);
                }
                 
            }

            if (other.CompareTag("Obstacle"))
            {
                collectableManager.OnDecreaseStack();
                Destroy(other.transform.parent);
            }

            if (other.CompareTag("DroneArea")) collectableManager.StartPointDroneArea();

            if (other.CompareTag("TurretArea")) collectableManager.StartPointTurretArea();

            // if (other.CompareTag("nextIdleLevel")) collectableMenager.onHitNextIdleLevel();

            if (other.CompareTag("Bullet"))
            {
                collectableManager.OnDecreaseStack();
                collectableManager.WhenCollectableDie();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            //if (other.CompareTag("buildingTextArea")) collectableMenager.OnDecreaseStack();
            if (other.CompareTag("TurretArea")) collectableManager.EndPointTaretArea();
        }
        
    }
}