using Command;
using Controllers;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Veriables

        #region SerializeField Variables

        [SerializeField] private UIPanelController UIPanelController;
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private TextMeshProUGUI textMeshPro2;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private CalculateMultiplyCommand CalculateMultiply;

        #endregion SerializeField Variables

        #endregion Self Veriables

        #region Event Subcription

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onMultiplyArea += OnMultiplyArea;
            //UISignals.Instance.onSetLevelText += OnSetLevelText;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onFailed += OnFailed;
            ScoreSignals.Instance.onSendUIScore += OnUpdateCurrentScore;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onMultiplyArea -= OnMultiplyArea;
           // UISignals.Instance.onSetLevelText -= OnSetLevelText;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onFailed -= OnFailed;
            ScoreSignals.Instance.onSendUIScore -= OnUpdateCurrentScore;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion Event Subcription

        #region PanelControls

        private void OnOpenPanel(UIPanels panels)
        {
            UIPanelController.OpenPanel(panels);
        }

        private void OnClosePanel(UIPanels panels)
        {
            UIPanelController.ClosePanel(panels);
        }

        #endregion PanelControls

        #region UnityEvent Methods

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

        #endregion UnityEvent Methods

        #region ButtonGroup

        public void Play()
        {
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
            CoreGameSignals.Instance.onChangeGameState(GameStates.Idle);
            ScoreSignals.Instance.onMultiplyAmaunt(CalculateMultiply.SelectMultiply());
        }

        public void NextLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.IdlePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onChangeGameState(GameStates.Runner);
            CoreGameSignals.Instance.onNextLevel?.Invoke();
        }

        public void Restart()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onChangeGameState(GameStates.Runner);
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void Vibration()
        {
            CameraSignals.Instance.onVibrateStatus?.Invoke();
        }

        #endregion ButtonGroup

        #region TextGroup

        private void OnUpdateCurrentScore(int score)
        {
            textMeshPro.text = score.ToString();
            textMeshPro2.text = score.ToString();
        }

        private void OnSetLevelText(int nextLevel)
        {
            nextLevel++;
            levelText.text = "Level " + nextLevel;
        }

        #endregion TextGroup
    }
}