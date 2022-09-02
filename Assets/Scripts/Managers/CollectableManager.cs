using UnityEngine;
using Signals;
using Controllers;
using System;
using DG.Tweening;
using Enums;
using Task = System.Threading.Tasks.Task;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Veriables


        #region SerializeField Veriables

        [SerializeField]
        CollectableMeshController collectableMeshController;
        [SerializeField]
        CollectableAnimationController collectableAnimationController;
        [SerializeField]
        CollectableParticalController collectableParticalController;

        [SerializeField] private CollectablePhysicController collectablePhysicController;
        [SerializeField] 
        private CollectableMovementCommand collectableMovementCommand;

        
        #endregion

        #region Private Variables

        #endregion

        #region Public Variables
        
        public ColorType CurrentCollectableColorType;

        public ColorMatchType ColorMatchType;
        
        #endregion

        #endregion

        private void Start()
        {
            SetReferences(); 
        }
        #region Event Subscription


        #endregion
        private void SetReferences()
        {
            collectableMeshController.SetCollectableMaterial(CurrentCollectableColorType);
        }

        #region Stack Management

        public void IncreaseStack(GameObject gameObject)
        {

            StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);
            ScoreSignals.Instance.onUpdateScore?.Invoke(ScoreStatus.plus);

            DOVirtual.DelayedCall(.2f, () => { ChangeAnimationOnController(CollectableAnimType.Run); });

        }
        public void DecreaseStack()
        {
            
            StackSignals.Instance.onDecreaseStack?.Invoke(transform.GetSiblingIndex());
            ScoreSignals.Instance.onUpdateScore?.Invoke(ScoreStatus.minus);
            ParticalSignals.Instance.onCollectableDead?.Invoke(transform.position);
            gameObject.transform.SetParent(null);
            CameraSignals.Instance.onVibrateCam.Invoke();
        }

        
        #endregion

        #region On Drone Area Collectable Behaviours
        

        public async void IncreaseStackAfterDroneArea()
        {
            await Task.Delay(300);
                
            StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);
            
            ChangeOutline(false);

            DOVirtual.DelayedCall(.2f, () => { ChangeAnimationOnController(CollectableAnimType.Run); });
        }

        public void DecreaseStackAfterDroneArea()
        {
            ChangeAnimationOnController(CollectableAnimType.Dying);
            
            gameObject.transform.SetParent(null);
            
            Death();
            
            Destroy(gameObject,3f);
        }
       

        public void DeListFromStack()
        {
            StackSignals.Instance.onDecreaseStackOnDroneArea?.Invoke(transform.GetSiblingIndex());
        }
        
        public void SetCollectablePositionOnDroneArea(Transform groundTransform)
        {
            collectableMovementCommand.MoveToGround(groundTransform);
        }

        #endregion
        
        #region Collectable Visuals
        
        public void ChangeOutline(bool isOutlineActive)
        {
            collectableMeshController.OutlineChange(isOutlineActive);
        }

        public void OnChangeColor(ColorType colorType)
        {
            CurrentCollectableColorType = colorType;
            collectableMeshController.SetCollectableMaterial(CurrentCollectableColorType);
        }

        public  void IsHitRainbow()
        {
           
            collectableMeshController.ChangeScale();
            CoreGameSignals.Instance.onEnterMutiplyArea();
            collectableMeshController.SetCollectableMaterial(ColorType.Rainbow);
        }

        public void ChangeAnimationOnController(CollectableAnimType collectableAnimType)
        {
            collectableAnimationController.ChangeAnimationState(collectableAnimType);
        }

        #endregion

        public void Death()
        {
            collectablePhysicController.DeActivedCollider();
        }
        
        public void EnterTurretArea(GameObject gameObjectOther)
        {   
            collectableMeshController.CompareColorOnTurretArea(gameObjectOther, CurrentCollectableColorType);
            
        }
        public void SendCollectableTransform()
        {   
            ObstacleSignals.Instance.onEnterTurretArea(transform);
           
        }
    }
}