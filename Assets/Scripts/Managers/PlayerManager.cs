using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables

        [Space][SerializeField] private PlayerMovementController movementController;

        [SerializeField] private PlayerAnimationController animationController;

        [SerializeField] private PlayerMeshController playerMeshController;

        [SerializeField] private PlayerScoreController playerScoreController;



        [SerializeField] private GameObject scoreHolder;

        #endregion
        private GameStates _gameStates;
        #endregion

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
            ScoreSignals.Instance.onSendScore += OnSetScoreText;

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
            ScoreSignals.Instance.onSendScore -= OnSetScoreText;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #region Movement Controller

        private void OnActivateMovement() => movementController.EnableMovement();

        private void OnDeactiveMovement() => movementController.DeactiveMovement();

        private void OnGetRunnerInputValues(RunnerInputParams inputParams) => movementController.UpdateRunnerInputValue(inputParams);

        private void OnGetIdleInputValues(IdleInputParams inputParams) 
        { 
            movementController.UpdateIdleInputValue(inputParams); 
        } 

        #endregion

        #endregion

        private void OnChangeGameState(GameStates gameStates) 
        {
            _gameStates = gameStates;
            movementController.ChangeGameStates(_gameStates);
        }


        private void OnPlay() => movementController.IsReadyToPlay(true);
        private void OnFailed() => movementController.IsReadyToPlay(false);

        private void OnLevelSuccessful() => movementController.IsReadyToPlay(false);//OnReset,player need to command  controller.

        private async void OnEnterMutiplyArea()
        {
           
            ChangeForwardSpeeds(ChangeSpeedState.EnterMultipleArea);
            await Task.Delay(1500);
            ChangeForwardSpeeds(ChangeSpeedState.Stop);
            playerMeshController.ChangeScale(1);
            playerScoreController.OnChangeScorePos();


        }
        public async void IsHitRainbow()
        {
           
            await Task.Delay(3500);//�t Will cahange
            UISignals.Instance.onMultiplyArea?.Invoke();
            
        }

        public void IsHitCollectable()
        {   
            if (_gameStates == GameStates.Idle)
            {
                ScoreSignals.Instance.onIncreaseScore?.Invoke();
            }
        }

        internal void IsEnterPaymentArea()
        {
            if (_gameStates == GameStates.Idle)
            {
                ScoreSignals.Instance.onDecreaseScore?.Invoke();
                playerMeshController.ChangeScale(-1);
            }
           
        }


        private void OnSetScoreText(int score) => playerScoreController.UpdateScore(score);

        public void OnStopVerticalMovement() => movementController.StopVerticalMovement();
        private void OnReset()
        {
            movementController.MovementReset();
            gameObject.SetActive(false);//changed

        }
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


        // IEnumerator WaitForFinal()
        // {
        //     animationController.Playanim(animationStates:PlayerAnimationStates.Idle);
        //     yield return new WaitForSeconds(2f);
        //     gameObject.SetActive(false);
        //     CoreGameSignals.Instance.onMiniGameStart?.Invoke();
        // }


    }
}