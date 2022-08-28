using UnityEngine;
using Signals;
using Enums;
using System;
using Controllers;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

namespace Managers
{

    public class UIManager : MonoBehaviour
    {


        #region Self Veriables

        #region SerializeField Variables

        [SerializeField] UIPanelController UIPanelController;
        [SerializeField] TextMeshProUGUI textMeshPro;
        [SerializeField] TextMeshProUGUI textMeshPro2;
        [SerializeField] RectTransform rectTransform;
        [SerializeField] RectTransform rectTransformCursor;
        [SerializeField] TextMeshProUGUI levelText;

        #endregion

        #region Private Variables
        private string _multiply;
        #endregion
        #endregion

        #region Event Subcription

        private void OnEnable()
        {
            SubscribeEvents();

        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onMultiplyArea += OnMultiplyArea;
            UISignals.Instance.onSetLevelText += OnSetLevelText;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onFailed += OnFailed;
            ScoreSignals.Instance.onSendScore += OnUpdateCurrentScore;
           


        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onMultiplyArea -= OnMultiplyArea;
            UISignals.Instance.onSetLevelText -= OnSetLevelText;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onFailed -= OnFailed;
            ScoreSignals.Instance.onSendScore -= OnUpdateCurrentScore;
           

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        #region PanelControls

        private void OnOpenPanel(UIPanels panels)
        {
            UIPanelController.OpenPanel(panels);
        }

        private void OnClosePanel(UIPanels panels)
        {
            UIPanelController.ClosePanel(panels);
        }
        #endregion

        public void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }
        public void OnFailed()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        }

        public void OnMultiplyArea()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.MultiplyPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.IdlePanel);
            CursorMovement();
        }

      
        #region ButonGroup
        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke(); // Invoker
        }

        

        public void TryAgain()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
        }
        public void EnterIdleArea()
        {

            UISignals.Instance.onClosePanel?.Invoke(UIPanels.MultiplyPanel);
            CoreGameSignals.Instance.onChangeGameState(GameStates.Idle);
        }

        public void NextLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel); // LevelPanel Acilmadi ekledik
            CoreGameSignals.Instance.onNextLevel?.Invoke();
        }

        public void Restart()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void Vibration()
        {
            
            CameraSignals.Instance.onVibrateStatus?.Invoke();
        }

        #region TextGroup
    
        private void OnUpdateCurrentScore(int score)
        {


            // textMeshPro.text = score.ToString();
            // textMeshPro2.text = score.ToString();

        }
        private void OnSetLevelText(int nextLevel)
        {
            nextLevel++;
            levelText.text = $"Level{nextLevel.ToString()}";
            #endregion
            #endregion

        }
        public void CursorMovement()
        {
            Sequence sequence = DOTween.Sequence();


            sequence.Join(rectTransform.DORotate(new Vector3(0, 0, 15), 1f).SetEase(Ease.Linear));//x2 lerin konumuna 
            sequence.Join(rectTransform.DOLocalMoveX(-320, 1f).SetEase(Ease.Linear));

            sequence.SetLoops(-1, LoopType.Yoyo).onPlay();


        }
        public void SelectMultiply()
        {
            float CursorXPos = rectTransform.localPosition.x;

            if (190 < CursorXPos && CursorXPos < 320)
            {
                _multiply = "x2";
            }
            else if (60 < CursorXPos && CursorXPos < 190)
            {
                _multiply = "x3";
            }
            else if (-50 < CursorXPos && CursorXPos < 60)
            {
                _multiply = "x5";
            }
            else if (-180 < CursorXPos && CursorXPos < -50)
            {
                _multiply = "x3";
            }
            else if (-320 < CursorXPos && CursorXPos < -180)
            {
                _multiply = "x2";
            }
            ScoreSignals.Instance.onMultiplyAmaunt?.Invoke(_multiply);
        }


    }



}

