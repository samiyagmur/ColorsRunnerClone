﻿using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Managers;
using ToonyColorsPro.ShaderGenerator;
using Unity.Mathematics;
using UnityEngine;

namespace Controllers{

public class PlayerMovementController : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables

    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private GameStates currentGameState;
    #endregion
    #region Private Variables
    [Header("Data")] private PlayerMovementData _movementData;
    private bool _isReadyToMove, _isReadyToPlay,_isMovingVertical;
    private float _inputValueX;
    private Vector2 _clampValues;
    private Vector3 _movementDirection;
    #endregion
    #endregion
    
        public void SetMovementData(PlayerMovementData dataMovementData)
        {   
            _movementData = dataMovementData;
            _movementData.ForwardSpeed = 10;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }

        public void UpdateRunnerInputValue(RunnerInputParams inputParam)
        {
            _inputValueX = inputParam.XValue;
            _clampValues = inputParam.ClampValues;
        }
        
        public void UpdateIdleInputValue(IdleInputParams inputParam)
        {
            _movementDirection = inputParam.InputValues;
        }
        

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }

        public void ChangeGameStates(GameStates currentState)
        {
            currentGameState = currentState;
        }
        private void Update()
        {
            if (_isReadyToPlay)
            {
                manager.SetStackPosition();
        
            }
        }
    
        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                
                if (_isReadyToMove)
                {
                    if (currentGameState == GameStates.Runner)
                    {
                       
                        RunnerMove();

                    }
                    else if (currentGameState == GameStates.Idle)
                    {
                        
                        IdleMove();
                    }
                    
                }
                else
                {
                    if (currentGameState == GameStates.Runner)
                    {
                        RunnerStopSideways();
                    }
                    else if (currentGameState == GameStates.Idle)
                    {
                        Stop();
                    }
                    
                }
                
            }
            else
                Stop();
        }

        private void RunnerMove()
        {
            var velocity = rigidbody.velocity;
                velocity = new Vector3(_inputValueX * _movementData.SidewaysSpeed, velocity.y,
                    _movementData.ForwardSpeed);
                rigidbody.velocity = velocity;

                Vector3 position;
                position = new Vector3(
                    Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                        _clampValues.y),
                    (position = rigidbody.position).y,
                    position.z);
                rigidbody.position = position;
            
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(_inputValueX,0,_movementData.ForwardSpeed*4));
            
                transform.rotation = toRotation;
                
        }

        private void IdleMove()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_movementDirection.x * _movementData.ForwardSpeed, velocity.y,
                _movementDirection.z * _movementData.ForwardSpeed);
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3(rigidbody.position.x, (position = rigidbody.position).y, position.z);
            rigidbody.position = position;

            if (_movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(_movementDirection);
                
                transform.rotation = toRotation;
            }
            
        }
        private void RunnerStopSideways()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _movementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }
        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        public void OnStopVerticalMovement()
        {
            _movementData.ForwardSpeed = 0;
            rigidbody.angularVelocity = Vector3.zero;
        }
        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }

       
    }
}