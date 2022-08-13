using System.Collections;
using Enums;
using UnityEngine;
using Managers;
using Signals;

namespace Controllers
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
                    collectableManager.OnIcreaseStack(other.transform.parent.gameObject);
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

            if (other.CompareTag("DroneArea")) collectableManager.ChangeAnimationOnController(CollectableAnimType.CrouchIdle); // Delay

            if (other.CompareTag("TurretArea")) collectableManager.ChangeAnimationOnController(CollectableAnimType.CrouchWalk);

            // if (other.CompareTag("nextIdleLevel")) collectableMenager.onHitNextIdleLevel();

            if (other.CompareTag("Bullet"))
            {
                collectableManager.OnDecreaseStack();
                collectableManager.ChangeAnimationOnController(CollectableAnimType.Dying);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            //if (other.CompareTag("buildingTextArea")) collectableMenager.OnDecreaseStack();
            if (other.CompareTag("TurretArea")) collectableManager.ChangeAnimationOnController(CollectableAnimType.Run);
        }
        
    }
}