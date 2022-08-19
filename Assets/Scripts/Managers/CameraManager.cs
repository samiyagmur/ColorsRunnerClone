using System.Collections;
using UnityEngine;
using Signals;
using System;
using Cinemachine;
using Controllers;
using DG.Tweening;
using Enums;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {

        #region Self Variables
        #region SerilizeField
        [SerializeField]CameraMovementController cameraMovementController;
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private PlayerManager playerManager;
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
            CoreGameSignals.Instance.onPlay += SetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onPlay -= SetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
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

        private void SetCameraTarget()
        {
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke();
        }
        private void OnSetCameraTarget()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            
            stateDrivenCamera.Follow = playerManager.transform;
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