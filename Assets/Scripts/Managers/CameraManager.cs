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
        #region Private Variables
        CamVibrationStatus vibrationStatus;
        private bool IsPressVibrating=true;


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
            CoreGameSignals.Instance.onReset +=OnReset;
            CoreGameSignals.Instance.onPlay += SetCameraTarget;
            CoreGameSignals.Instance.onReset += SetCameraTarget;
            CoreGameSignals.Instance.onChangeGameState += OnEnterIdleArea;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterMutiplyArea += OnEnterMultiplyArea;
            CameraSignals.Instance.onVibrateCam += OnVibrateCam;
            CameraSignals.Instance.onVibrateStatus += OnVibrateStatus;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onPlay -= SetCameraTarget;
            CoreGameSignals.Instance.onReset -= SetCameraTarget;
            CoreGameSignals.Instance.onChangeGameState -= OnEnterIdleArea;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterMutiplyArea -= OnEnterMultiplyArea;
            CameraSignals.Instance.onVibrateCam -= OnVibrateCam;
            CameraSignals.Instance.onVibrateStatus -= OnVibrateStatus;
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
            stateDrivenCamera.Follow = playerManager.transform;


            //Debug.Log(stateDrivenCamera.Follow.name);


        }
        private void OnReset()
        {
            cameraMovementController.WhenOnReset();

        }

        private void OnVibrateStatus()
        {
            Debug.Log("vib");
           
            if (IsPressVibrating)
            {
                vibrationStatus = CamVibrationStatus.Active;
                IsPressVibrating = false;
            }
            else
            {
                vibrationStatus = CamVibrationStatus.Pasive;
                IsPressVibrating = true;
            }
            Debug.Log(IsPressVibrating);
            vibrationStatus = CamVibrationStatus.Pasive;//Inactive
        }

        private void OnVibrateCam()
        {
            if (vibrationStatus== CamVibrationStatus.Active)
            {
                transform.DOPunchRotation(new Vector3(0.5f, 0.5f, 0.5f), 1f, 10, 1f);

            }
            
            
            
        }

    }
}