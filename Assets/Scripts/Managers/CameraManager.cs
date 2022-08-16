using System.Collections;
using UnityEngine;
using Signals;
using System;
using Cinemachine;
using Controllers;
using DG.Tweening;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {

        #region Self Variables
        #region SerilizeField
        [SerializeField]CameraMovementController cameraMovementController;
        [SerializeField] private CinemachineVirtualCamera levelCam;
        [SerializeField] private PlayerManager playerManager;
        #endregion
        #endregion

        #region Event Subcription

        private void Start()
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        private void OnEnable()
        {
            SubscribeEvents();

        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onEnterDroneArea += OnEnterDroneArea;
            CoreGameSignals.Instance.onExitDroneArea += OnExitDroneArea;
            CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onEnterDroneArea -= OnEnterMiniGame;
            CoreGameSignals.Instance.onExitDroneArea += OnExitDroneArea;
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

        private void OnEnterDroneArea()
        {
            levelCam.Follow = null;
        }
        private void OnEnterMiniGame()
        {
            cameraMovementController.WhenEnterMiniGame();
        }

        private void OnExitDroneArea()
        {   
            DOVirtual.DelayedCall(.1f,()=>levelCam.Follow = playerManager.transform);
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