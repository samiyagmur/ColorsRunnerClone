using Datas.ValueObject;
using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controlers{

public class PlayerMovementController : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables

    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Rigidbody rigidbody;
    #endregion
    #region Private Variables
    [Header("Data")] private PlayerMovementData _movementData;
    private GameStates _currentGameState = GameStates.Idle;
    private bool _isReadyToMove, _isReadyToPlay;
    private float _inputValueX,_inputValueZ;
    private Vector2 _clampValues;
    private Vector3 _movementDirection;
    #endregion
    #endregion
    
    public void SetMovementData(PlayerMovementData dataMovementData)
        {   
            _movementData = dataMovementData;
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
            _inputValueX = inputParam.XValue;
            _inputValueZ = inputParam.ZValue;
            _movementDirection = new Vector3(_inputValueX,0,_inputValueZ);
        }
        
        


        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
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
                    if (_currentGameState == GameStates.Runner)
                    {
                        RunnerMove();
                    }
                    else if (_currentGameState == GameStates.Idle)
                    {
                        Debug.Log("İdle");
                        IdleMove();
                    }
                    
                }
                else
                {
                    if (_currentGameState == GameStates.Runner)
                    {
                        RunnerStopSideways();
                    }
                    else if (_currentGameState == GameStates.Idle)
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
        }

        private void IdleMove()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValueX * _movementData.ForwardSpeed, velocity.y,
                _inputValueZ * _movementData.ForwardSpeed);
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3(rigidbody.position.x, (position = rigidbody.position).y, position.z);
            rigidbody.position = position;
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
        
        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}