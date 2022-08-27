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
        private MultipleAreaStatus _multipleAreaStatus;
        private int _score;
        private int _variantScore=1;
        private int _multiplyValue;
        private bool _isPressClaimButton;
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
            ScoreSignals.Instance.onDecreaseScore += OnDecreaseScore;
            ScoreSignals.Instance.onMultiplyAmaunt += OnMultiplyAmaunt;
            CoreGameSignals.Instance.onEnterMutiplyArea += OnEnterMutiplyArea;
            CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
            CoreGameSignals.Instance.onReset += OnReset;

        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onIncreaseScore -= OnIncreaseScore;
            ScoreSignals.Instance.onDecreaseScore -= OnDecreaseScore;
            ScoreSignals.Instance.onMultiplyAmaunt += OnMultiplyAmaunt;
            CoreGameSignals.Instance.onEnterMutiplyArea -= OnEnterMutiplyArea;
            CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion



        private void OnEnterMutiplyArea()
        {

            _multipleAreaStatus = MultipleAreaStatus.active;

        }
        private void OnEnterIdleArea()
        {
            
            _multipleAreaStatus = MultipleAreaStatus.pasive;
        }

        private void OnIncreaseScore()
        {
            _score++;
            SendScoreOrMultiplyValue(_score);
        }

        private void OnDecreaseScore()
        {
            if (_score<0)
            {
                _score = 0;
            }
            _score--;
            SendScoreOrMultiplyValue(_score);
        }
        private void OnMultiplyAmaunt(string value)
        {
            _isPressClaimButton = true;
            
            _multiplyValue = Int32.Parse(value.TrimStart('x'));
           
            _score *= _multiplyValue;
      

            SendScoreOrMultiplyValue(_score);


        }

        private void SendScoreOrMultiplyValue(int score)
        {
          

            if (_multipleAreaStatus==MultipleAreaStatus.active)
            {
                if (!_isPressClaimButton)
                {
                    score = _variantScore++;
                    _score = score;
                }
             
                ScoreSignals.Instance.onSendScore?.Invoke(score);
            }
            else
            {
              
                ScoreSignals.Instance.onSendScore?.Invoke(score);
            }
            
            
        }


        private void OnReset()
        {
            _score = 0;
        }






    }
}