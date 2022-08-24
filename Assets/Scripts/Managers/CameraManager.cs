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
            CoreGameSignals.Instance.onReset += SetCameraTarget;
            CoreGameSignals.Instance.onChangeGameState += OnEnterIdleArea;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterMutiplyArea += OnEnterMultiplyArea;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onPlay -= SetCameraTarget;
            CoreGameSignals.Instance.onReset -= SetCameraTarget;
            CoreGameSignals.Instance.onChangeGameState -= OnEnterIdleArea;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterMutiplyArea -= OnEnterMultiplyArea;
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

        private void OnEnterMultiplyArea()
        {
            cameraMovementController.WhenEnterMultiplyArea();
        }

        private void OnEnterIdleArea(GameStates arg0)
        {
            cameraMovementController.WhenEnTerIdleArea();
            stateDrivenCamera.LookAt=playerManager.transform;


            //Debug.Log(stateDrivenCamera.Follow.name);


        }
        private void OnReset()
        {
            cameraMovementController.WhenOnReset();

        }
    }
}