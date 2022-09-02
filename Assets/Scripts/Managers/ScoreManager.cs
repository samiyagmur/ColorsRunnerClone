using Enums;
using Signals;
using System;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Veriables

        #region Private Veriables

        private int _totalScore;
        private ScoreStatus state;
        private bool IsEnterMultiplyArea;
        private int IdleScore;
        #endregion Private Veriables

        #endregion Self Veriables
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

            
            ScoreSignals.Instance.onUpdateScore += onUpdateScore;
            CoreGameSignals.Instance.onEnterMutiplyArea += OnEnterMultilyArea;
            ScoreSignals.Instance.onMultiplyAmaunt += OnMultiplyAmaunt;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {

           
            ScoreSignals.Instance.onUpdateScore -= onUpdateScore;
            CoreGameSignals.Instance.onEnterMutiplyArea -= OnEnterMultilyArea;
            ScoreSignals.Instance.onMultiplyAmaunt -= OnMultiplyAmaunt;
            CoreGameSignals.Instance.onReset -= OnReset;
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        private void OnEnterMultilyArea()
        {

            IsEnterMultiplyArea = true;
        }
        
        private void OnMultiplyAmaunt(string value)
        {
            
            IdleScore *= Convert.ToInt32(value.TrimStart('x'));
            EnterIdleArea();

        }
        private void EnterIdleArea()
        {
             _totalScore = IdleScore;
            IsEnterMultiplyArea = false;
            ReadUIText(_totalScore);
            ReadPlayerText(_totalScore);

        }
        private void onUpdateScore(ScoreStatus state)
        {

            if (_totalScore <= 0)
            {   
                _totalScore = 0;
            }

            if (state== ScoreStatus.plus)
            {
                _totalScore ++;
                ReadPlayerText(_totalScore);
                ReadUIText(_totalScore);
            }
            else
            {
                if (IsEnterMultiplyArea)
                {
                    _totalScore--;
                    IdleScore++;
                    ReadPlayerText(_totalScore);
                    ReadUIText(IdleScore);
                }
                else
                {
                    _totalScore--;
                    ReadPlayerText(_totalScore);
                    ReadUIText(_totalScore);
                }
            }
        }

        private void ReadUIText(int _totalScore)
        {
            ScoreSignals.Instance.onSendUIScore?.Invoke(_totalScore);
        }

        private void ReadPlayerText(int _totalScore)
        {
            ScoreSignals.Instance.onSendPlayerScore?.Invoke(_totalScore);
        }

        private void OnReset()
        {
            IdleScore = 0;
            _totalScore = 0;
            ReadPlayerText(_totalScore);
        }

    }
}





//private void Start()
//{
//    _scoreStatus = ScoreStatusAsLocations.LevelInitilize;
//}

//private void OnEnterMutiplyArea()
//{
//    _scoreStatus = ScoreStatusAsLocations.EnterMultiple;
//}

//private void OnEnterIdleArea()
//{
//    _scoreStatus = ScoreStatusAsLocations.EnterIdle;
//    CalculateScore();

//}

//private void OnEnterPaymentArea()
//{
//    _scoreStatus = ScoreStatusAsLocations.EnterPaymentArea;
//}

//private void OnExitPaymentArea()
//{
//    _scoreStatus = ScoreStatusAsLocations.ExitPaymentArea;
//}

//private void OnReset()
//{
//    _scoreStatus = ScoreStatusAsLocations.Reset;
//    CalculateScore();
//}





//private void CalculateScore()
//{
//    switch (_scoreStatus)
//    {
//        case ScoreStatusAsLocations.LevelInitilize:
//            ReadPlayerText(_score);
//            break;

//        case ScoreStatusAsLocations.Reset:
//            ReadPlayerText(_score);
//            break;

//        case ScoreStatusAsLocations.EnterMultiple:
//            _totalScore++;
//            ReadPlayerText(_score);
//            ReadUIText(_totalScore);
//            break;

//        case ScoreStatusAsLocations.EnterIdle:
//            Debug.Log(_totalScore);
//            _idleScore += _totalScore;
//            _idleScore += _score;
//            ReadUIText(_idleScore);
//            ReadPlayerText(_idleScore);
//            _score = 0;
//            _totalScore = 0;
//            break;

//        case ScoreStatusAsLocations.EnterPaymentArea:
//            ScoreSignals.Instance.onSendPlayerScore(_idleScore);
//            BuildingSignals.Instance.onActiveTextUpdate.Invoke();
//            _idleScore--;
//            if (_idleScore <= 0)
//            {
//                BuildingSignals.Instance.onScoreZero.Invoke();
//                _idleScore = 0;
//            }
//            ReadPlayerText(_idleScore);
//            ReadUIText(_idleScore);
//            break;

//        case ScoreStatusAsLocations.ExitPaymentArea:
//            _idleScore = _score;
//            ReadPlayerText(_idleScore);
//            ReadUIText(_idleScore);
//            break;
//    }
//}