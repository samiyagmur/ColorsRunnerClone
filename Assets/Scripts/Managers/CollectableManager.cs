using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Signals;
using Controlers;
using System;
using Datas.ValueObject;
using Datas.UnityObject;
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
        [SerializeField]
        private ColorType collectableColorType;
        #endregion

        #region Private Variables

        #endregion

        #region Public Variables
        
        #endregion

        #endregion

        private void Awake()
        {
            collectableMeshController.GetCollectableMaterial(collectableColorType);
        }

        #region Physical Managment
        public void OnIcreaseStack() { StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);}

        public void OnDecreaseStack() 
        { 
            StackSignals.Instance.onDecreaseStack?.Invoke(transform.GetSiblingIndex());
            collectableParticalController.PlayPartical();
        }
        public void OnChangeColor(ColorType colorType) =>collectableMeshController.GetCollectableMaterial(colorType);
        public void StartPointTurretArea() => collectableAnimationController.WhenEnterTaretArea();
        public void EndPointTaretArea() => collectableAnimationController.WhenExitTaretArea();
        public void StartPointDroneArea() => collectableAnimationController.Invoke("WhenEnterDronArea",2f);

        //ObstacleBilgigelcek ona göre ayaða kalkçak;

        #endregion
    }
}