using Controllers;
using Controllers.BuildingControllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        public int ScoreVaryant;

        #endregion Public Variables

        #region Serialized Variables

        [Space][SerializeField] private PlayerMovementController movementController;

        [SerializeField] private PlayerAnimationController animationController;

        [SerializeField] private PlayerMeshController playerMeshController;

        [SerializeField] private PlayerScoreController playerScoreController;

        [SerializeField] private PlayerThrowController playerThrowController;

        [SerializeField] private GameObject scoreHolder;

        [SerializeField] private Rigidbody playerRigidbody;

        [SerializeField] private CapsuleCollider playerCollider;

        #endregion Serialized Variables

        private GameStates _gameStates;
        private PlayerAnimationType playerAnimation;
        

        #endregion Self Variables

        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToControllers();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.MovementData);
            movementController.ChangeForwardSpeed(ChangeSpeedState.Normal);
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onInputDragged += OnGetRunnerInputValues;
            InputSignals.Instance.onJoyStickInputDragged += OnGetIdleInputValues;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onFailed += OnFailed;
            CoreGameSignals.Instance.onEnterMutiplyArea += OnEnterMutiplyArea;
            CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
            ScoreSignals.Instance.onSendPlayerScore += OnSetScoreText;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onInputDragged -= OnGetRunnerInputValues;
            InputSignals.Instance.onJoyStickInputDragged -= OnGetIdleInputValues;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onFailed -= OnFailed;
            CoreGameSignals.Instance.onEnterMutiplyArea -= OnEnterMutiplyArea;
            CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;
            ScoreSignals.Instance.onSendPlayerScore -= OnSetScoreText;
        }

        public void SetupScore()
        {
            ScoreSignals.Instance.onUpdateScore?.Invoke(ScoreStatus.minus);
            playerMeshController.ChangeScale(-1);
            ChangePlayerAnimation(PlayerAnimationType.Throw);
        }

        public void PlayerPhysicDisabled()
        {
            playerCollider.enabled = false;
            playerRigidbody.useGravity = false;
        }

        

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #region Movement Controller

        private void OnActivateMovement()
        {
           ChangePlayerAnimation(PlayerAnimationType.Running);

            movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();

            ChangePlayerAnimation(PlayerAnimationType.Idle);
        }

        public void ExitPaymentArea()
        {
            ParticalSignals.Instance.onParticleStop?.Invoke();
        }

        private void OnGetRunnerInputValues(RunnerInputParams inputParams) => movementController.UpdateRunnerInputValue(inputParams);

        private void OnGetIdleInputValues(IdleInputParams inputParams)
        {
            movementController.UpdateIdleInputValue(inputParams);
        }

        #endregion Movement Controller

        #endregion Event Subscription

        private void OnChangeGameState(GameStates gameStates)
        {
            _gameStates = gameStates;
            movementController.ChangeGameStates(_gameStates);
        }

        private void OnPlay() => movementController.IsReadyToPlay(true);

        private void OnFailed() => movementController.IsReadyToPlay(false);

        private void OnReset()
        {
            movementController.MovementReset();
            gameObject.SetActive(false);//changed
            movementController.ChangeHorizontalSpeed(HorizontalSpeedStatus.Active);
        }

        private async void OnEnterMutiplyArea()
        {
            ChangeForwardSpeeds(ChangeSpeedState.EnterMultipleArea);
            await Task.Delay(1500);
            ChangeForwardSpeeds(ChangeSpeedState.Stop);
            movementController.ChangeHorizontalSpeed(HorizontalSpeedStatus.Pasive);
            playerMeshController.ChangeScale(1);
            playerScoreController.OnChangeScorePos();
        }

        private void OnEnterIdleArea()
        {
            movementController.ChangeHorizontalSpeed(HorizontalSpeedStatus.Active);
        }

        public async void IsHitRainbow()
        {
            await Task.Delay(3500);//it Will cahange
            UISignals.Instance.onMultiplyArea?.Invoke();
        }

        public void IsHitCollectable()
        {
            if (_gameStates == GameStates.Idle)
            {
                ScoreSignals.Instance.onUpdateScore?.Invoke(ScoreStatus.plus);
            }
        }

        private void OnSetScoreText(int score)
        {
            ScoreVaryant = score;
            playerMeshController.CalculateSmallerRate(score);
            PlayerScoreText(score);
        }
        private void PlayerScoreText(int score)=> playerScoreController.UpdateScore(score);
        public void OnStopVerticalMovement() => movementController.StopVerticalMovement();

        public void SendToColorType(ColorType colorType) => StackSignals.Instance.onChangeColor?.Invoke(colorType);

        public void DeActivateScore(bool isActive)
        {
            scoreHolder.SetActive(isActive);
        }

        public async void StartMovementAfterDroneArea(Transform exitPosition)
        {
            StartVerticalMovement(exitPosition);
            await Task.Delay(1000);
        }

        public void StartVerticalMovement(Transform exitPosition) => movementController.OnStartVerticalMovement(exitPosition);

        public void ChangeForwardSpeeds(ChangeSpeedState changeSpeedState) => movementController.ChangeForwardSpeed(changeSpeedState);

        public void ChangePlayerAnimation(PlayerAnimationType animType)
        {
            animationController.ChangeAnimationState(animType);
        }

    }
}