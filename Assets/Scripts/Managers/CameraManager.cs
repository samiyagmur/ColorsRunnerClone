using System.Collections;
using UnityEngine;
using Signals;
using System;
using Controlers;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {

        #region Self Variables
        #region SerilizeField
        [SerializeField]CamMovementController camMovementController;
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
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onEnterMiniGame += OnEnterMiniGame;
            CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onEnterMiniGame -= OnEnterMiniGame;
            CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnPlay()
        {
            camMovementController.whenGameStart();
        }
    

        private void OnEnterMiniGame()
        {
            camMovementController.WhenEnterMiniGame();
        }

        private void OnEnterIdleArea()
        {
            camMovementController.WhenEnTerIdleArea();
        }
        private void OnReset()
        {
            camMovementController.WhenOnReset();
        }








    }
}