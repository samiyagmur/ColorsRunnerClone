using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject initStack; //Data
        
        [SerializeField] private List<GameObject> collectableList = new List<GameObject>();
        
        [SerializeField] [Range(0.02f, 1f)] private float lerpDelay; //Data
        
        [SerializeField] private Transform playerTransform;
        
        [SerializeField] private int initSize = 3;  //Pooldan cek // Data

        [SerializeField] private GameObject stackHolder;  
        
        #endregion

        #endregion

        private void Start()
        {
            OnInitializeStack();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += SetStackTarget;
            StackSignals.Instance.onSetStackTarget += OnSetStackTarget;
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
            StackSignals.Instance.onDecreaseStackOnDroneArea += OnDecreaseStackOnDroneArea;
            StackSignals.Instance.onChangeColor += OnChangeCollectableColor;
            StackSignals.Instance.onChangeCollectedAnimation += OnChangeCollectedAnimation;
            StackSignals.Instance.onInitializeStack += OnRunStack;
        }

        private void UnsubscribeEvents()
        {   
            CoreGameSignals.Instance.onPlay -= SetStackTarget;
            StackSignals.Instance.onSetStackTarget -= OnSetStackTarget;
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack-= OnDecreaseStack;
            StackSignals.Instance.onDecreaseStackOnDroneArea -= OnDecreaseStackOnDroneArea;
            StackSignals.Instance.onChangeColor -= OnChangeCollectableColor;
            StackSignals.Instance.onChangeCollectedAnimation -= OnChangeCollectedAnimation;
            StackSignals.Instance.onInitializeStack-= OnRunStack;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SetStackTarget()
        {
            StackSignals.Instance.onSetStackTarget?.Invoke();
        }

        private void OnSetStackTarget()
        {

            playerTransform = FindObjectOfType<PlayerManager>().transform;
            
            stackHolder = GameObject.FindWithTag("StackHolder");
            
            StackSignals.Instance.onInitializeStack?.Invoke();
            
            
        }
        private void FixedUpdate()
        {
            if (playerTransform != null)
            {
                LerpStackWithMathf();
            }
           
        }
        
        #region Initialize Stack

        private void OnInitializeStack()
        {
            for (int i = 0; i < initSize ; i++)
            {
                var _currentStack = Instantiate(initStack, Vector3.zero, this.transform.rotation);
                
                AddStackOnInitialize(_currentStack);
                
                StackSignals.Instance.onChangeCollectedAnimation?.Invoke(CollectableAnimType.CrouchIdle);
                
            }
        }

        private void OnRunStack()
        {
          
           StackSignals.Instance.onChangeCollectedAnimation?.Invoke(CollectableAnimType.Run);
            
        }
        
        private void AddStackOnInitialize(GameObject currentStack)
        {
            collectableList.Add(currentStack);
            
            currentStack.transform.SetParent(transform); // all currenstack need to change
            
            currentStack.transform.position = collectableList[collectableList.Count-1].transform.position + Vector3.back;
        }
        

        #endregion

        #region Stack Visuals

        private void OnChangeCollectedAnimation(CollectableAnimType nextCollectableAnimType)
        {
            for (int i = 0; i < collectableList.Count; i++)
            {
                collectableList[i].GetComponent<CollectableManager>().ChangeAnimationOnController(nextCollectableAnimType);
            }
        }
        
        private void OnDoubleStack()
        {
            // Work in Progress,When Pool is created,we will add this future for beloved users.
        }
        private async void OnChangeCollectableColor(ColorType colorType)
        {   
            for (int i = 0; i < collectableList.Count; i++)
            {
                await Task.Delay(50);
                collectableList[i].GetComponent<CollectableManager>().OnChangeColor(colorType);
            }
        }

        #endregion

        #region Stack / Unstack Collectables

        private async void OnIncreaseStack(GameObject currentStack) 
        {
            
            collectableList.Add(currentStack);
            
            currentStack.transform.SetParent(transform);

            if (collectableList.Count == 1)
            {
                playerTransform.position = collectableList[0].transform.position;
            }

            await Task.Delay(100);
            currentStack.SetActive(true);
        }

        private void OnDecreaseStack(int currentIndex)
        {
            collectableList.RemoveAt(currentIndex);
            
            collectableList.TrimExcess();
        }

        #endregion

        #region Decrease Stack On Drone Area
        
        private void SetDroneAreaHolder(GameObject gameObject)
        {
            gameObject.transform.SetParent(stackHolder.transform); // atmiycam
        }
        private async void OnDecreaseStackOnDroneArea(int currentIndex)  
        {
            SetDroneAreaHolder(collectableList[currentIndex].gameObject);
            
            collectableList[currentIndex].transform.SetParent(stackHolder.transform);

            collectableList.RemoveAt(currentIndex);
            
            collectableList.TrimExcess();
            
            DroneAreaSignals.Instance.onDroneActive?.Invoke();
            
            if(transform.childCount == 0)
            {   
                await Task.Delay(1000);
                
                DroneAreaSignals.Instance.onDisableAllColliders?.Invoke();
               
                await Task.Delay(3000);
                
                DroneAreaSignals.Instance.onEnableDroneAreaCollider?.Invoke(); 
                
                await Task.Delay(50);
                
                DroneAreaSignals.Instance.onDisableDroneAreaCollider?.Invoke();
                DroneAreaSignals.Instance.onDisableWrongColorGround?.Invoke();
            }
        }
        

        #endregion

        #region Decrease stack On LevelEnd

        private async void OnLevelEndDecreaseStack()
        {
            for (int i = 0; i < collectableList.Count; i++)
            {   
                collectableList[i].transform.SetParent(stackHolder.transform);
                collectableList.RemoveAt(i);
                
            }
        }

        #endregion
        
        #region Collectable Lerp Position && Rotation
        
        private void LerpStackWithMathf()
        {
            for (int i = 0; i < collectableList.Count; i++) 
            {   
                if (i == 0)
                {
                    var _collectablePos = collectableList.ElementAt(i);
                    Vector3 _targetPos = playerTransform.position;
        
                    _collectablePos.transform.position= new Vector3(
                        Mathf.Lerp(_collectablePos.transform.position.x, _targetPos.x, 0.3f),
                        Mathf.Lerp(_collectablePos.transform.position.y, _targetPos.y, 0.3f),
                        Mathf.Lerp(_collectablePos.transform.position.z,  _targetPos.z- 1f, lerpDelay));
                    
                    Vector3 _rotationDirection = _targetPos - _collectablePos.transform.position;
                    if (_rotationDirection != Vector3.zero)
                    {
                        Quaternion _toRotation = Quaternion.LookRotation(_rotationDirection);
                   
                        _toRotation = Quaternion.Euler(0,_toRotation.eulerAngles.y,0);
                        _collectablePos.transform.rotation = Quaternion.Slerp(_collectablePos.transform.rotation,_toRotation,1f);
                    }
                }
                else
                {
                    var _collectablePos = collectableList.ElementAt(i-1);
                    var _targetPos = collectableList.ElementAt(i);
                    _targetPos.transform.position = new Vector3(
                        Mathf.Lerp(_targetPos.transform.position.x, _collectablePos.transform.position.x, 0.3f),
                        Mathf.Lerp(_targetPos.transform.position.y, _collectablePos.transform.position.y, 0.3f),
                        Mathf.Lerp(_targetPos.transform.position.z, _collectablePos.transform.position.z - 1f, lerpDelay));

                    Vector3 rotationAngle = _collectablePos.transform.position - _targetPos.transform.position;
                    
                    if (rotationAngle != Vector3.zero)
                    {
                        Quaternion _toRotation = Quaternion.LookRotation(rotationAngle);
   
                        _toRotation = Quaternion.Euler(0,_toRotation.eulerAngles.y,0);
                        _targetPos.transform.rotation = Quaternion.Slerp(_collectablePos.transform.rotation,_toRotation,1f);
                    }
                }
            }
        }

      
        #endregion
        
    }
}
