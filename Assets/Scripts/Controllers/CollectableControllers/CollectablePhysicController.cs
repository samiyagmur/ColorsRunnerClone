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
        
        [SerializeField] private BoxCollider collectableCollider;

        #endregion

        #endregion
        
        
        private void OnTriggerEnter(Collider other)
        {   
            #region Stacking collectables
            
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
            #endregion

            #region Obstacle Collision

            if (other.CompareTag("Obstacle"))
            {
                collectableManager.DecreaseStack();
                Destroy(other.transform.gameObject);
               
            }

            #endregion

            #region DroneArea Collisions
            
            
            #region Enter Colored Ground
            
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

            #endregion


            #region Exit Colored Ground
            
            if (other.CompareTag("AfterGround"))
            {
                if (collectableManager.ColorMatchType != ColorMatchType.Match)
                {   
                    collectableManager.DecreaseStackAfterDroneArea();
                    
                }
                else
                {
                    collectableManager.IncreaseStackAfterDroneArea();
                    gameObject.tag = "Collected";
                }
            }


            #endregion


            #endregion

            if (other.CompareTag("Rainbow")) collectableManager.IsHitRainbow();


            if (other.CompareTag("TurretArea")) collectableManager.ChangeAnimationOnController(CollectableAnimType.CrouchWalk);

            // if (other.CompareTag("nextIdleLevel")) collectableMenager.onHitNextIdleLevel();

            if (other.CompareTag("Bullet")) //else if or return, there is no need to loop all tags
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

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("TurretArea"))
            {
                collectableManager.EnterTurretArea(other.gameObject);
            }
        }
        
        public void DeActivedCollider()
        {
            collectableCollider.enabled = false;
        }

    }
}