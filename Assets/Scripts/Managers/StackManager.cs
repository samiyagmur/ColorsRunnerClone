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
        
        [SerializeField] [Range(0.02f, 1f)] private float lerpDelay; // Data
        
        [SerializeField] private Transform playerTransform; // Levelden buldur
        
        [SerializeField] private int initSize = 3;  //Pooldan cek // Data

        [SerializeField] private GameObject stackHolder;       //Levelden Buldur,test amacli boyle kalabilir
        
        

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
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
            StackSignals.Instance.onDecreaseStackOnDroneArea += OnDecreaseStackOnDroneArea;
            StackSignals.Instance.onChangeColor += OnChangeCollectableColor;
            StackSignals.Instance.onChangeCollectedAnimation += OnChangeCollectedAnimation;
            CoreGameSignals.Instance.onGameOpen+= OnInitializeStack;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack-= OnDecreaseStack;
            StackSignals.Instance.onDecreaseStackOnDroneArea -= OnDecreaseStackOnDroneArea;
            StackSignals.Instance.onChangeColor -= OnChangeCollectableColor;
            StackSignals.Instance.onChangeCollectedAnimation -= OnChangeCollectedAnimation;
            CoreGameSignals.Instance.onGameOpen += OnInitializeStack;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void FixedUpdate()
        {
           LerpStackWithMathf();
        }

        #region Initialize Stack

        private void OnInitializeStack()
        {
            for (int i = 0; i < initSize ; i++)
            {   
                
                var _currentStack = Instantiate(initStack, Vector3.zero, this.transform.rotation); // change name
                
                AddStackOnInitialize(_currentStack);
                
                StackSignals.Instance.onChangeCollectedAnimation?.Invoke(CollectableAnimType.CrouchIdle);
                
            }
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

        private void OnIncreaseStack(GameObject currentStack) 
        {
            
            collectableList.Add(currentStack);
            
            currentStack.transform.SetParent(transform);

            

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
            gameObject.transform.SetParent(stackHolder.transform);
        }
        private async void OnDecreaseStackOnDroneArea(int currentIndex)  
        {
            SetDroneAreaHolder(collectableList[currentIndex].gameObject);
            
            collectableList[currentIndex].transform.SetParent(stackHolder.transform);

            collectableList.RemoveAt(currentIndex);
            
            collectableList.TrimExcess();

            if (transform.childCount == 0)
            {
                await Task.Delay(3000);
                
                DroneAreaSignals.Instance.onDisableAllColliders?.Invoke();
                
                DroneAreaSignals.Instance.onEnableDroneAreaCollider?.Invoke();
                
                await Task.Delay(500);
                
                DroneAreaSignals.Instance.onDisableDroneAreaCollider?.Invoke();
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
                        Mathf.Lerp(_collectablePos.transform.position.x, _targetPos.x, 0.2f),
                        Mathf.Lerp(_collectablePos.transform.position.y, _targetPos.y, 0.2f),
                        Mathf.Lerp(_collectablePos.transform.position.z,  _targetPos.z- 1.5f, lerpDelay));
                     
                    Quaternion _toRotation = Quaternion.LookRotation(_targetPos - _collectablePos.transform.position);
                    _toRotation = Quaternion.Euler(0,_toRotation.eulerAngles.y,0);
                    _collectablePos.transform.rotation = Quaternion.Slerp(_collectablePos.transform.rotation,_toRotation,1f);
                }
                else
                {
                    var _collectablePos = collectableList.ElementAt(i-1);
                    var _targetPos = collectableList.ElementAt(i);
                    _targetPos.transform.position = new Vector3(
                        Mathf.Lerp(_targetPos.transform.position.x, _collectablePos.transform.position.x, 0.2f),
                        Mathf.Lerp(_targetPos.transform.position.y, _collectablePos.transform.position.y, 0.2f),
                        Mathf.Lerp(_targetPos.transform.position.z, _collectablePos.transform.position.z - 1.5f, lerpDelay));
                     
                    Quaternion _toRotation = Quaternion.LookRotation(_collectablePos.transform.position - _targetPos.transform.position);
                    _toRotation = Quaternion.Euler(0,_toRotation.eulerAngles.y,0);
                    _targetPos.transform.rotation = Quaternion.Slerp(_collectablePos.transform.rotation,_toRotation,1f);

                }
            }
        }
        #endregion
        
    }
}
