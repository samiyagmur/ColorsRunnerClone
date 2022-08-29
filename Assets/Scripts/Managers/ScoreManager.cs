using Signals;
using UnityEngine;
using Enums;
using System;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {

        #region Self Veriables
        #region Private Veriables
        ScoreStatusAsLocations scoreStatus;
        private int Score;
        private int TotalScore;
        private int IdleScore;
        private int MultiplyAmaunt;

        #endregion
        #endregion
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
        
            CoreGameSignals.Instance.onEnterMutiplyArea += OnEnterMutiplyArea;
            CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onEnterPaymentArea += OnEnterPaymentArea;
            ScoreSignals.Instance.onIncreaseScore += OnIncreaseScore;
            ScoreSignals.Instance.onDecreaseScore += OnDecreaseScore;
            ScoreSignals.Instance.onMultiplyAmaunt += OnMultiplyAmaunt;


        }

        private void UnsubscribeEvents()
        {
      
            CoreGameSignals.Instance.onEnterMutiplyArea -= OnEnterMutiplyArea;
            CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onEnterPaymentArea -= OnEnterPaymentArea;
            ScoreSignals.Instance.onIncreaseScore -= OnIncreaseScore;
            ScoreSignals.Instance.onDecreaseScore -= OnDecreaseScore;
            ScoreSignals.Instance.onMultiplyAmaunt -= OnMultiplyAmaunt;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        private void Start()
        {
            
            scoreStatus=ScoreStatusAsLocations.LevelInitilize;
        }
        private void OnEnterMutiplyArea()
        {
            
            scoreStatus = ScoreStatusAsLocations.EnterMultiple;
            Debug.Log("OnEnterMutiplyArea");
        }

        private void OnEnterIdleArea()
        {
            
            Debug.Log("OnEnterIdleArea");
            scoreStatus = ScoreStatusAsLocations.ExitMultiple;
        }

        private void OnEnterPaymentArea()
        {
            Debug.Log("OnEnterPaymentArea");
            scoreStatus = ScoreStatusAsLocations.EnterPaymentArea;
        }

        private void OnNextLevel()
        {
          
            scoreStatus = ScoreStatusAsLocations.NextLevel;

            Debug.Log("OnNextLevel");
        }

        private void OnReset()
        {
         
            scoreStatus = ScoreStatusAsLocations.Reset;

        }

        private void OnIncreaseScore()
        {
            Score++;
            CalculateScore();
        }

        private void OnDecreaseScore()
        {
            Score--;
            if (Score < 0)
            {
                Score = 0;
            }
           
            CalculateScore();
        }

        private void OnMultiplyAmaunt(string value)
        {
            scoreStatus = ScoreStatusAsLocations.ExitMultiple;
            MultiplyAmaunt = Convert.ToInt32(value.TrimStart('x'));
            TotalScore *= MultiplyAmaunt;
            CalculateScore();
        }
        
        private void CalculateScore()
        {
            switch (scoreStatus)
            {
                case ScoreStatusAsLocations.LevelInitilize:
                    ReadPlayerText(Score);
                    break;
                case ScoreStatusAsLocations.NextLevel:
                    ReadPlayerText(Score);               
                    break;
                case ScoreStatusAsLocations.Reset:
                    ReadPlayerText(Score);
                    break;
                case ScoreStatusAsLocations.EnterMultiple:
                    TotalScore++;
                    ReadPlayerText(Score);
                    ReadUIText(TotalScore);
                    break;
                case ScoreStatusAsLocations.ExitMultiple:
                    IdleScore += TotalScore;
                    IdleScore += Score;
                    ReadUIText(IdleScore);
                    ReadPlayerText(IdleScore);
                    Score = 0;
                    TotalScore =0;
                    break;
                case ScoreStatusAsLocations.EnterPaymentArea:
                    ScoreSignals.Instance.onSendPlayerScore(IdleScore);
                    IdleScore--;
                    if (IdleScore <= 0)
                    {
                        IdleScore = 0;
                    }
                    ReadPlayerText(IdleScore);
                    ReadUIText(IdleScore);

                    break;
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


    }
}
//#region Self Veriables
//#region Private Veriables
//private MultipleAreaStatus _multipleAreaStatus;
//private int _uıScore;
//private int _playerScore;
//private int _variantScore=1;
//private int _multiplyValue;
//private bool _isPressClaimButton;
//private bool IsPressNextLevel;
//#endregion
//#endregion

//#region Event Subscription
//private void OnEnable()
//{
//    SubscribeEvents();
//}
//private void SubscribeEvents()
//{
//    ScoreSignals.Instance.onIncreaseScore += OnIncreaseScore;
//    ScoreSignals.Instance.onDecreaseScore += OnDecreaseScore;
//    ScoreSignals.Instance.onMultiplyAmaunt += OnMultiplyAmaunt;
//    CoreGameSignals.Instance.onEnterMutiplyArea += OnEnterMutiplyArea;
//    CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
//    CoreGameSignals.Instance.onNextLevel += OnNextLevel;
//    CoreGameSignals.Instance.onReset += OnReset;

//}

//private void UnsubscribeEvents()
//{
//    ScoreSignals.Instance.onIncreaseScore -= OnIncreaseScore;
//    ScoreSignals.Instance.onDecreaseScore -= OnDecreaseScore;
//    ScoreSignals.Instance.onMultiplyAmaunt += OnMultiplyAmaunt;
//    CoreGameSignals.Instance.onEnterMutiplyArea -= OnEnterMutiplyArea;
//    CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;,
//     CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
//    CoreGameSignals.Instance.onReset -= OnReset;
//}
//private void OnDisable()
//{
//    UnsubscribeEvents();
//}

//#endregion



//private void OnEnterMutiplyArea()
//{

//    _multipleAreaStatus = MultipleAreaStatus.active;

//}
//private void OnEnterIdleArea()
//{

//    _multipleAreaStatus = MultipleAreaStatus.pasive;
//}

//private void OnIncreaseScore()
//{

//    _uıScore++;
//    _playerScore++;
//    SendScoreOrMultiplyValue(_uıScore, _playerScore);
//}

//private void OnDecreaseScore()
//{

//    if (_playerScore < 0)
//    {
//        _uıScore = 0;
//    }
//    _uıScore--;
//    _playerScore--;
//    SendScoreOrMultiplyValue(_uıScore, _playerScore);
//}
//private void OnMultiplyAmaunt(string value)
//{
//    _isPressClaimButton = true;

//    _multiplyValue = Int32.Parse(value.TrimStart('x'));

//    _uıScore *= _multiplyValue;
//    _playerScore *= _multiplyValue;

//    SendScoreOrMultiplyValue(_uıScore, _playerScore);


//}

//private void SendScoreOrMultiplyValue(int uıScore,int playerScore)
//{


//    if (_multipleAreaStatus==MultipleAreaStatus.active)
//    {

//        if (!_isPressClaimButton)
//        {
//            _uıScore = uıScore++;
//            _playerScore= playerScore++;
//            _uıScore = uıScore;
//            _playerScore = playerScore;

//        }

//        ScoreSignals.Instance.onSendUIScore?.Invoke(uıScore);
//        ScoreSignals.Instance.onSendPlayerScore?.Invoke(playerScore);
//    }
//    else
//    {


//        ScoreSignals.Instance.onSendUIScore?.Invoke(uıScore);
//        ScoreSignals.Instance.onSendPlayerScore?.Invoke(playerScore);
//    }

//}


//private void OnNextLevel()
//{
//    IsPressNextLevel = true;
//}

//private void OnReset()
//{

//    if (IsPressNextLevel)
//    {
//        _uıScore = uıScore;
//    }
//    else
//    {

//        _playerScore = 0;
//    }


//}