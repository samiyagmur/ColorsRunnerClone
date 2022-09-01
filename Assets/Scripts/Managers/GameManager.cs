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

        private void Awake()
        {
            Application.targetFrameRate = 60;
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
    }
}
