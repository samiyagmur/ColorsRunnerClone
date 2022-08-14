using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using System;
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
        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private GameObject skinMeshRenderer;
        
        
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
            movementController.IsReadyToPlay(false);

        }
        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }

        public void SetStackPosition()
        {
            Vector2 pos = new Vector2(transform.position.x,transform.position.z);
           // StackSignals.Instance.onStackFollowPlayer?.Invoke(pos);
        }
        internal void SendToColorType(ColorType colorType)
        {
            StackSignals.Instance.onChangeColor?.Invoke(colorType);
        }
        private void OnReset()
        {
            gameObject.SetActive(true);
            movementController.OnReset();
            skinMeshRenderer.GetComponent<SkinnedMeshRenderer>().enabled = false;
            gameObject.GetComponent<PlayerAnimationController>().enabled = false;
        }

        private void OnSetScoreText(int Values)
        {
            scoreText.text = Values.ToString();
        }

        public void OnStopVerticalMovement()
        {
            movementController.OnStopVerticalMovement();
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