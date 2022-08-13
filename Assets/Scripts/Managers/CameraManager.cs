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
        [SerializeField]CameraMovementController cameraMovementController;
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
            cameraMovementController.whenGameStart();
        }

        private void OnEnterMiniGame()
        {
            cameraMovementController.WhenEnterMiniGame();
        }

        private void OnEnterIdleArea()
        {
            cameraMovementController.WhenEnTerIdleArea();
        }
        private void OnReset()
        {
            cameraMovementController.WhenOnReset();
        }
    }
}