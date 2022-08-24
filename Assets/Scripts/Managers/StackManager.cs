using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        #region Private Variables
        private MultipyStatus _multipyStatus;
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
            CoreGameSignals.Instance.onReset += OnReset;
            StackSignals.Instance.onSetStackTarget += OnSetStackTarget;
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
            StackSignals.Instance.onDecreaseStackOnDroneArea += OnDecreaseStackOnDroneArea;
            StackSignals.Instance.onChangeColor += OnChangeCollectableColor;
            StackSignals.Instance.onChangeCollectedAnimation += OnChangeCollectedAnimation;
            StackSignals.Instance.onInitializeStack += OnRunStack;
            CoreGameSignals.Instance.onEnterMutiplyArea += OnEnterMutiplyArea;
            
        }

        private void UnsubscribeEvents()
        {   
            CoreGameSignals.Instance.onPlay -= SetStackTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
            StackSignals.Instance.onSetStackTarget -= OnSetStackTarget;
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack-= OnDecreaseStack;
            StackSignals.Instance.onDecreaseStackOnDroneArea -= OnDecreaseStackOnDroneArea;
            StackSignals.Instance.onChangeColor -= OnChangeCollectableColor;
            StackSignals.Instance.onChangeCollectedAnimation -= OnChangeCollectedAnimation;
            StackSignals.Instance.onInitializeStack-= OnRunStack;
            CoreGameSignals.Instance.onEnterMutiplyArea -= OnEnterMutiplyArea;
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
                LerpStack();
            }
            
            
           
        }
        
        #region Initialize Stack

        private void OnInitializeStack()
        {
            for (int i = 0; i < initSize ; i++)
            {
                var _currentStack = Instantiate(initStack, new Vector3(0,0,0-i), this.transform.rotation);
                
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
        private  void OnChangeCollectableColor(ColorType colorType)
        {   
            for (int i = 0; i < collectableList.Count; i++)
            {
                //await Task.Delay(50);
                collectableList[i].GetComponent<CollectableManager>().OnChangeColor(colorType);
            }
        }

        #endregion

        #region Stack / Unstack Collectables

        private async void OnIncreaseStack(GameObject currentStack) 
        {
            
            collectableList.Add(currentStack);
            
            collectableList.TrimExcess();
            
            currentStack.transform.SetParent(transform);

            if (collectableList.Count == 1)
            {
                playerTransform.position = collectableList[0].transform.position;
            }
            currentStack.SetActive(false);
            await Task.Delay(25);
            currentStack.SetActive(true);
        }

        private void OnDecreaseStack(int currentIndex)
        {
            if (collectableList[currentIndex] is null)
            {   
                return;
            }

            GameObject currentGameObj = collectableList[currentIndex].gameObject;
            
            collectableList[currentIndex].gameObject.SetActive(false);
            
            collectableList.RemoveAt(currentIndex);
            
            Destroy(currentGameObj,0.1f);
            
            collectableList.TrimExcess();
            if (_multipyStatus == MultipyStatus.Pasive)
            {
                OnFail(collectableList.Count);
            }
            
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
                await Task.Delay(350);
              


                OnFail(collectableList.Count);
            }
        }


        #endregion

        #region Decrease stack On LevelEnd

        private  void OnLevelEndDecreaseStack()//async silindi
        {
            for (int i = 0; i < collectableList.Count; i++)
            {   
                collectableList[i].transform.SetParent(stackHolder.transform);
                collectableList.RemoveAt(i);
                
            }
        }

        #endregion
        
        #region Collectable Lerp Position && Rotation
        
        private void LerpStack()
        {
            for (int i = 0; i < collectableList.Count; i++) 
            {   
                if (i == 0)
                {
                    if (collectableList[i] is null)
                    {
                        return;
                    }

                    var position = playerTransform.position;
                    collectableList[i].transform.position= new Vector3(
                        Mathf.Lerp(collectableList[i].transform.position.x, position.x, 0.3f),
                        Mathf.Lerp(collectableList[i].transform.position.y, position.y, 0.3f),
                        Mathf.Lerp(collectableList[i].transform.position.z,  position.z- 1f, lerpDelay));
                    
                    Vector3 rotationDirection = position - collectableList[i].transform.position;
                    
                    if (rotationDirection != Vector3.zero)
                    {
                        Quaternion toRotation = Quaternion.LookRotation(rotationDirection);
                        
                        toRotation = Quaternion.Euler(0,toRotation.eulerAngles.y,0);
                        
                        collectableList[i].transform.rotation = Quaternion.Slerp(collectableList[i].transform.rotation,toRotation,1f);
                    }
                }
                else
                {
                    if (collectableList[i] is null || collectableList[i-1] is null)
                    {
                        return;
                    }
                    collectableList[i].transform.position = new Vector3(
                        Mathf.Lerp(collectableList[i].transform.position.x, collectableList[i-1].transform.position.x, 0.3f),
                        Mathf.Lerp(collectableList[i].transform.position.y, collectableList[i-1].transform.position.y, 0.3f),
                        Mathf.Lerp(collectableList[i].transform.position.z, collectableList[i-1].transform.position.z - 1f, lerpDelay));

                    Vector3 rotationDirection = collectableList[i-1].transform.position - collectableList[i].transform.position;
                    
                    if (rotationDirection != Vector3.zero)
                    {
                        Quaternion toRotation = Quaternion.LookRotation(rotationDirection);
   
                        toRotation = Quaternion.Euler(0,toRotation.eulerAngles.y,0);
                        
                        collectableList[i].transform.rotation = Quaternion.Slerp(collectableList[i].transform.rotation,toRotation,1f);
                    }
                }
            }
        }

        private void OnFail(int count)
        {
            if (count is 0)
            {
                CoreGameSignals.Instance.onFailed();
            }

        }

        private void OnEnterMutiplyArea()
        {
            _multipyStatus = MultipyStatus.Active;
        }


        private void OnReset()
        {
            OnInitializeStack();
        }

        #endregion

    }
}
