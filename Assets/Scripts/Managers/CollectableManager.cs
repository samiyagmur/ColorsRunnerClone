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
            CollectableEnterStack();
        }

        public void OnDecreaseStack() 
        { 
            StackSignals.Instance.onDecreaseStack?.Invoke(transform.GetSiblingIndex());
            gameObject.transform.SetParent(null);
            collectableParticalController.PlayPartical();
            WhenCollectableDie();
            Destroy(gameObject,1.5f);
        }

        public void GameOpenStack()
        {
            collectableAnimationController.WhenGameOpenStack();
        }

        public void GameStartStack()
        {
            collectableAnimationController.WhenPlay();
        }
        public void OnChangeColor(ColorType colorType) =>collectableMeshController.SetCollectableMaterial(colorType);

        private void CollectableEnterStack() => collectableAnimationController.WhenEnterStack();
        public void StartPointTurretArea() => collectableAnimationController.WhenEnterTaretArea();
        public void EndPointTaretArea() => collectableAnimationController.WhenExitTaretArea();
        public void StartPointDroneArea() => collectableAnimationController.Invoke("WhenEnterDronArea",2f);
        
        public void WhenCollectableDie() => collectableAnimationController.WhenCollectableDie();
        
        
        
        
        

        #endregion
    }
}