using System;
using Datas.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Private Variables

        private GameStates States;

        #endregion

        #region Serialized Variables

        

        #endregion

        #endregion


        #region Event Subscription

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void OnEnable()
        {
            OnGameOpen();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnChangeGameState(GameStates newState)
        {
            States = newState;
        }

        private void OnApplicationPause(bool isPauseStatus)
        {
            if (isPauseStatus)
            {
                CoreGameSignals.Instance.onApplicationPause?.Invoke();
            }
            
        }

        private void OnApplicationQuit()
        {
            CoreGameSignals.Instance.onApplicationQuit?.Invoke();
        }

        private void OnGameOpen()
        {
            CoreGameSignals.Instance.onGameOpen?.Invoke();
        }

    }
}
