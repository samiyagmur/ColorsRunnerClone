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
            CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterMutiplyArea += OnEnterMultiplyArea;
            //CameraSignals.Instance.onVibrateCam += OnVibrateCam;
            //CameraSignals.Instance.onVibrateStatus += OnVibrateStatus;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterMutiplyArea -= OnEnterMultiplyArea;
           // CameraSignals.Instance.onVibrateCam -= OnVibrateCam;
            //CameraSignals.Instance.onVibrateStatus -= OnVibrateStatus;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region Evet Methods
        private void OnPlay()
        {
            cameraMovementController.whenGameStart();
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke();
        }

        private void OnEnterMultiplyArea()
        {
            cameraMovementController.WhenEnterMultiplyArea();
        }

        private void OnEnterIdleArea()
        {

            cameraMovementController.WhenEnTerIdleArea();
            //stateDrivenCamera.Follow = playerManager.transform;
        }
        private void OnReset()
        {
            cameraMovementController.WhenOnReset();
            CoreGameSignals.Instance.onSetCameraTarget?.Invoke();

        }


        //private void OnVibrateStatus()
        //{

        //    if (IsPressVibrating)
        //    {
        //        vibrationStatus = CamVibrationStatus.Active;
        //        IsPressVibrating = false;
        //    }
        //    else
        //    {
        //        vibrationStatus = CamVibrationStatus.Pasive;
        //        IsPressVibrating = true;
        //    }

        //}

        //private void OnVibrateCam()
        //{
        //    if (vibrationStatus== CamVibrationStatus.Active)
        //    {
        //        transform.DOPunchRotation(new Vector3(0.5f, 0.5f, 0.5f), 1f, 10, 1f);

        //    }

        //}
        private void OnSetCameraTarget()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            stateDrivenCamera.Follow = playerManager.transform;
        }
        #endregion
    }
}