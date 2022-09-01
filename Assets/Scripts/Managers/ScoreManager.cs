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
            CoreGameSignals.Instance.onEnterPaymentArea += OnEnterPaymentArea;
            CoreGameSignals.Instance.onExitPaymentArea += OnExitPaymentArea;
            ScoreSignals.Instance.onIncreaseScore += OnIncreaseScore;
            ScoreSignals.Instance.onDecreaseScore += OnDecreaseScore;
            ScoreSignals.Instance.onMultiplyAmaunt += OnMultiplyAmaunt;


        }

        private void UnsubscribeEvents()
        {
      
            CoreGameSignals.Instance.onEnterMutiplyArea -= OnEnterMutiplyArea;
            CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onEnterPaymentArea -= OnEnterPaymentArea;
            CoreGameSignals.Instance.onExitPaymentArea -= OnExitPaymentArea;
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
           
        }

        private void OnEnterIdleArea()
        {
            scoreStatus = ScoreStatusAsLocations.EnterIdle;
            CalculateScore();
        }

        private void OnEnterPaymentArea()
        {
        
            scoreStatus = ScoreStatusAsLocations.EnterPaymentArea;
        }

  
        private void OnExitPaymentArea()
        {
          
            
            scoreStatus = ScoreStatusAsLocations.ExitPaymentArea;
        }

        private void OnReset()
        {
            scoreStatus = ScoreStatusAsLocations.Reset;
            CalculateScore();

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
            scoreStatus = ScoreStatusAsLocations.EnterIdle;
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
                case ScoreStatusAsLocations.Reset:
                    ReadPlayerText(Score);
                    break;
                case ScoreStatusAsLocations.EnterMultiple:
                    TotalScore++;
                    ReadPlayerText(Score);
                    ReadUIText(TotalScore);
                    break;
                case ScoreStatusAsLocations.EnterIdle:
                    IdleScore += TotalScore;
                    IdleScore += Score;
                    ReadUIText(IdleScore);
                    ReadPlayerText(IdleScore);
                    Score = 0;
                    TotalScore =0;
                    break;
                case ScoreStatusAsLocations.EnterPaymentArea:
                    ScoreSignals.Instance.onSendPlayerScore(IdleScore);
                    BuildingSignals.Instance.onActiveTextUpdate.Invoke();
                    IdleScore--;
                    if (IdleScore <= 0)
                    {
                        BuildingSignals.Instance.onScoreZero.Invoke();
                        IdleScore = 0;
                    }
                    ReadPlayerText(IdleScore);
                    ReadUIText(IdleScore);
                    break;
                case ScoreStatusAsLocations.ExitPaymentArea:
                    IdleScore=Score;
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