using System;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public GameStates States;

        #endregion

        #region Private Variables

        

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

        private void OnApplicationPause(bool pauseStatus)
        {
            throw new NotImplementedException();
        }

        private void OnApplicationQuit()
        {
            throw new NotImplementedException();
        }
    }
}
