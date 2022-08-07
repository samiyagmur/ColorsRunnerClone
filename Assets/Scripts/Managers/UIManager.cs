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

        [SerializeField] uiPanelController uiPanelController;

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

        private void OnOpenPanel(uiPanels panels)
        {
            uiPanelController.OpenPanel(panels);
        }

        private void OnClosePanel(uiPanels panels)
        {
            uiPanelController.ClosePanel(panels);
        }

        #endregion

        public void StartButton()
        {
            UISignals.Instance.onClosePanel?.Invoke(uiPanels.startPanel);

        }

        public void OnFail()
        {
            UISignals.Instance.onOpenPanel?.Invoke(uiPanels.failPanel);
            //CoreGamesignals.Instance.onReset?.Invoke();
        }

        public void OnEnterMiniGame()
        {
            UISignals.Instance.onClosePanel?.Invoke(uiPanels.levelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(uiPanels.miniGamePanel);
            UISignals.Instance.onOpenPanel?.Invoke(uiPanels.IdlePanel);
        }

        public void EnterIdleArea()
        {
            UISignals.Instance.onClosePanel?.Invoke(uiPanels.miniGamePanel);
        }

        public void TryAgain()
        {
            UISignals.Instance.onClosePanel?.Invoke(uiPanels.failPanel);
            UISignals.Instance.onOpenPanel?.Invoke(uiPanels.startPanel);
            // CoreGamesignals.Instance.onReset?.Invoke();
        }

        public void NextLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(uiPanels.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(uiPanels.levelPanel);
            //CoreGamesignals.Instance.onNextLevel?.Invoke();
        }

        public void Restart()
        {
            UISignals.Instance.onOpenPanel?.Invoke(uiPanels.startPanel);
            // CoreGamesignals.Instance.onReset?.Invoke();

        }
    }
}

