using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Signals;
using Controllers;
using System;
using Datas.ValueObject;
using Datas.UnityObject;
using DG.Tweening;
using Enums;

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

        [SerializeField] private CollectableMovementCommand collectableMovementCommand;

        public ColorType CurrentCollectableColorType;

        public ColorMatchType ColorMatchType;
        #endregion

        #region Private Variables

        #endregion

        #region Public Variables

        #endregion

        #endregion

        private void Start()
        {
            SetReferences();
        }

        private void SetReferences()
        {
            collectableMeshController.SetCollectableMaterial(CurrentCollectableColorType);
        }

        #region Physical Managment

        public void IncreaseStack(GameObject gameObject)
        {
            StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);
            DOVirtual.DelayedCall(.2f, () => { ChangeAnimationOnController(CollectableAnimType.Run); });

        }

        public void DecreaseStack()
        {
            StackSignals.Instance.onDecreaseStack?.Invoke(transform.GetSiblingIndex());
            gameObject.transform.SetParent(null);
            //collectableParticalController.PlayPartical();
            Destroy(gameObject);
        }

        public void DeListFromStack()
        {
            StackSignals.Instance.onDecreaseStackOnDroneArea?.Invoke(transform.GetSiblingIndex());
        }

        public void DeathOnArea()
        {
            ChangeAnimationOnController(CollectableAnimType.Dying);
            Destroy(gameObject,2f);
        }

        public void SetCollectablePositionOnDroneArea(Transform groundTransform)
        {
            collectableMovementCommand.MoveToGround(groundTransform);
            collectableMeshController.OutlineChange();
        }

        public void OnChangeColor(ColorType colorType)
        {
            CurrentCollectableColorType = colorType;
            collectableMeshController.SetCollectableMaterial(CurrentCollectableColorType);
        }

        public void ChangeAnimationOnController(CollectableAnimType collectableAnimType)
        {
            

            collectableAnimationController.ChangeAnimationState(collectableAnimType);
        
        
        }
        public void EnterTurretArea(Material materialOther)
        {
            Debug.Log(collectableMeshController.GetComponent<Renderer>().material);
            if (collectableMeshController.GetComponent<Renderer>().material.color==materialOther.color)
            {
                Debug.Log("girdi");
                ObstacleSignals.Instance.onEnterTurretArea?.Invoke(transform);
            }
            
            
        }
        

        #endregion
    }
}