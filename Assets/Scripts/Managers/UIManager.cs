using UnityEngine;
using Signals;
using Enums;
using System;
using Controlers;

namespace Managers
{

    public class UIManager : MonoBehaviour
    {


        #region Self Veriables

        #region SerializeField Veriables

        [SerializeField] UIPanelController UIPanelController;

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
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
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
       
        
        public void OnFail()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        }

        public void OnEnterMiniGameArea()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.MultiplyPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.IdlePanel);
        }

        #region ButonGrup
        public void StartButton()
        {
            Debug.Log("Tetik");
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onPlay?.Invoke();
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
            CoreGameSignals.Instance.onEnterIdleArea();
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
    } 
        #endregion
}

