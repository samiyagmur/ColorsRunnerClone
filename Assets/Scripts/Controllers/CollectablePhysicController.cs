using System;
using System.Collections;
using DG.Tweening;
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
            if (CompareTag("Collected") && other.CompareTag("Collectable"))
            {
                CollectableManager otherCollectableManager = other.transform.parent.GetComponent<CollectableManager>();
                
                if (collectableManager.CurrentCollectableColorType == otherCollectableManager.CurrentCollectableColorType)
                {
                    otherCollectableManager.IncreaseStack(other.transform.parent.gameObject);
                    other.tag = "Collected";
                   
                }
                else
                {
                    collectableManager.DecreaseStack();
                    other.transform.parent.gameObject.SetActive(false);
        
                }
                 
            }

            if (other.CompareTag("Obstacle"))
            {
                collectableManager.DecreaseStack();
                Destroy(other.transform.parent);
                
            }
            
            if (other.CompareTag("DroneArea"))
            {
                collectableManager.DeListFromStack();
            }

            if (other.CompareTag("ColoredGround") && CompareTag("Collected"))
            {
                
               
               collectableManager.SetCollectablePositionOnDroneArea(other.gameObject.transform);
              
               if (collectableManager.CurrentCollectableColorType == other.GetComponent<GroundColorCheckController>().colorType)
               {
                   collectableManager.ColorMatchType = ColorMatchType.Match;
               }
               
               else
               {
                   collectableManager.ColorMatchType = ColorMatchType.Unmatch;
               }
               tag = "Collectable";

            }
           

            if (other.CompareTag("TurretArea")) collectableManager.ChangeAnimationOnController(CollectableAnimType.CrouchWalk);

            // if (other.CompareTag("nextIdleLevel")) collectableMenager.onHitNextIdleLevel();

            if (other.CompareTag("Bullet"))
            {
                collectableManager.DecreaseStack();
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