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
       
        public ColorType CollectableColorType;
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
            collectableMeshController.SetCollectableMaterial(CollectableColorType);
        }

        #region Physical Managment

        public void OnIcreaseStack()
        {
            StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);
            ChangeAnimationOnController(CollectableAnimType.Run);
        }

        public void OnDecreaseStack() 
        { 
            StackSignals.Instance.onDecreaseStack?.Invoke(transform.GetSiblingIndex());
            gameObject.transform.SetParent(null);
            collectableParticalController.PlayPartical();
            ChangeAnimationOnController(CollectableAnimType.Dying);
            Destroy(gameObject,1.5f);
        }


        public void OnChangeColor(ColorType colorType) =>collectableMeshController.SetCollectableMaterial(colorType);
        
        public void ChangeAnimationOnController(CollectableAnimType collectableAnimType) => collectableAnimationController.ChangeAnimationState(collectableAnimType);
        
 

        #endregion
    }
}