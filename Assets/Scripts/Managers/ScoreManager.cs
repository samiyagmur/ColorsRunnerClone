using Enums;
using Signals;
using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Veriables
        #region Private Veriables

        private int _score;

        #endregion
        #endregion

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onIncreaseScore += OnIncreaseScore;
            ScoreSignals.Instance.onDecreaseScore += onDecreaseScore;

        }
        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onIncreaseScore -= OnIncreaseScore;
            ScoreSignals.Instance.onDecreaseScore -= onDecreaseScore;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnIncreaseScore()
        {
            _score++;
            SendScore(_score);
        }

        private void onDecreaseScore()
        {
            if (_score<0)
            {
                _score = 0;
            }
            _score--;
            SendScore(_score);
        }
        
        private void SendScore(int score) =>  ScoreSignals.Instance.onSendScore?.Invoke(score);
 


    }
}