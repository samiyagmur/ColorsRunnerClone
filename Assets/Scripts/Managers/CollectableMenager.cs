using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Signals;
using Controlers;
using System;

namespace Managers
{
    public class CollectableMenager : MonoBehaviour
    {
        #region Self Veriables
        #region SerializeField Veriables
        [SerializeField]
        CollectableMashController collectableMashController;
        [SerializeField]
        CollectableAnimationController collectableAnimationController;
        [SerializeField]
        CollectableParticalController collectableParticalController;

        #endregion
        #region Private Variables

        #endregion
        #endregion




        #region Event Subcription
        private void OnEnable()
        {
            SubscribeEvents();

        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onGameOpen += OnGameOpen;
            CoreGameSignals.Instance.onEnterMiniGame += OnEnterMiniGame;
            //CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onGameOpen -= OnGameOpen;
            CoreGameSignals.Instance.onEnterMiniGame = OnEnterMiniGame;
            //CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        #region Physical Managment
        public void OnIcreaseStack() { StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);}
        public void OnDecreaseStack() 
        { 
            StackSignals.Instance.onDecreaseStack?.Invoke(transform.GetSiblingIndex());
            collectableParticalController.PlayPartical();
        }
        public void OnChangeColor(Material material) { StackSignals.Instance.onMaterialChange?.Invoke(material);}
        private void OnGameOpen() {collectableAnimationController.WhenGameOpen();}
        private void OnPlay() { collectableAnimationController.WhenPlay(); }
        public void StartPointTurretArea() => collectableAnimationController.WhenEnterTaretArea();
        public void EndPointTaretArea() => collectableAnimationController.WhenExitTaretArea();
        public void StartPointDroneArea(Material materialDrone)
        {
            StackSignals.Instance.onMaterialChangeForDroneArea?.Invoke(materialDrone);//dornkontrol//Materyal kontrol edilcek.
            collectableAnimationController.WhenEnterDronArea();
        }
        public void EndPointDronArea()
        {
            //collectableMashController.reChangeOutLine();
            collectableAnimationController.WhenExitDronArea();
        }

        private void OnEnterMiniGame() { collectableAnimationController.WhenEnterMiniGame(); }
        //private void OnEnterIdleArea() { collectableAnimationController.WhenEnterIdleArea(); }
        private void OnNextLevel() { collectableAnimationController.WhenNextLevel(); }  
      
        //public void onHitBuildingTextArea() => //IdlegameSignals ;

        //public void onHitNextIdleLevel() =>//IdlegameSignals


        #endregion
    }
}