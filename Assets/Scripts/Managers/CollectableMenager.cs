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
            CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
            
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onGameOpen -= OnGameOpen;
            CoreGameSignals.Instance.onEnterMiniGame = OnEnterMiniGame;
            CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        #region Physical Managment
        public void OnIcreaseStack() { StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);}
        public void OnDecreaseStack() { StackSignals.Instance.onDecreaseStack?.Invoke(GetInstanceID());}
        public void OnChangeColor(Material material) => collectableMashController.changeColor(material);
        private void OnGameOpen() { collectableAnimationController.WhenGameOpen(); }
        private void OnPlay() { collectableAnimationController.WhenPlay(); }
        public void StartPointTaretArea() => collectableAnimationController.WhenEnterTaretArea();
        public void EndPointTaretArea() => collectableAnimationController.WhenExitTaretArea();
        public void StartPointDronArea()
        {
            collectableMashController.changeOutLine();
            collectableAnimationController.WhenEnterDronArea();
        }
        public void EndPointDronArea()
        {
            collectableMashController.reChangeOutLine();
            collectableAnimationController.WhenExitDronArea();
        }
        private void OnEnterMiniGame() { collectableAnimationController.WhenEnterMiniGame(); }
        private void OnEnterIdleArea() { collectableAnimationController.WhenEnterIdleArea(); }


        //public void onHitBuildingTextArea() => //IdlegameSignals ;

        //public void onHitNextIdleLevel() =>//IdlegameSignals


        #endregion
    }
}