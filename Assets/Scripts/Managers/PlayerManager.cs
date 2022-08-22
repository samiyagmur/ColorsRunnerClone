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

        [Space] [SerializeField] private PlayerMovementController movementController;
        
        [SerializeField] private PlayerAnimationController animationController;
        
        [SerializeField] private PlayerMeshController playerMeshController;

        [SerializeField] private TextMeshPro scoreText;


        #endregion
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
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #region Movement Controller

        private void OnActivateMovement()
        {
            movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
        }

        private void OnGetRunnerInputValues(RunnerInputParams inputParams)
        {
            movementController.UpdateRunnerInputValue(inputParams);
        }

        private void OnGetIdleInputValues(IdleInputParams inputParams)
        {
            movementController.UpdateIdleInputValue(inputParams);
        }

        #endregion

        #endregion

        private void OnChangeGameState(GameStates gameStates)
        {
            movementController.ChangeGameStates(gameStates);
        }
        
        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false); //OnReset,playermanager  emir versin,is yapmasin

        }
        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false); // Reset
        }
        
        internal void SendToColorType(ColorType colorType)
        {
            StackSignals.Instance.onChangeColor?.Invoke(colorType);
        }
        private void OnReset()
        {
            gameObject.SetActive(true);
            movementController.OnReset();
        }

        private void OnSetScoreText(int Values)
        {
            scoreText.text = Values.ToString();
        }

        public async void StartMovementAfterDroneArea(Transform exitPosition)
        {
            StartVerticalMovement(exitPosition);

            await Task.Delay(1000);
            

        }

        public void OnStopVerticalMovement()
        {
            movementController.StopVerticalMovement();
        }

        public void StartVerticalMovement(Transform exitPosition)
        {
            movementController.OnStartVerticalMovement(exitPosition);
        }

        // IEnumerator WaitForFinal()
        // {
        //     animationController.Playanim(animationStates:PlayerAnimationStates.Idle);
        //     yield return new WaitForSeconds(2f);
        //     gameObject.SetActive(false);
        //     CoreGameSignals.Instance.onMiniGameStart?.Invoke();
        // }
        public void ChangeForwardSpeeds(ChangeSpeedState changeSpeedState)
        {
            movementController.ChangeForwardSpeed(changeSpeedState);
        }




    }
}