using UnityEngine;
using Signals;
using Enums;
using System;
using Controllers;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;

namespace Managers
{

    public class UIManager : MonoBehaviour
    {


        #region Self Veriables

        #region SerializeField Veriables

        [SerializeField] UIPanelController UIPanelController;
        [SerializeField] TextMeshProUGUI textMeshPro;
        [SerializeField] TextMeshProUGUI textMeshPro2;
        
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
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onFailed += OnFailed;
            ScoreSignals.Instance.onSendScore += OnSendScore;


        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onMultiplyArea -= OnMultiplyArea;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onFailed -= OnFailed;
            ScoreSignals.Instance.onSendScore -= OnSendScore;

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
        }
        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke(); // Invoker
        }

        #region ButonGroup

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
            CoreGameSignals.Instance.onNextLevel?.Invoke();
        }

        public void Restart()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
        }
        #region TextGroup
    
        private void OnSendScore(int score)
        {


            textMeshPro.text = score.ToString();
            textMeshPro2.text = score.ToString();

        }
        #endregion
        #endregion
    }



}

