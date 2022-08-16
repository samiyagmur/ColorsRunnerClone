using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enums;
using Signals;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Private Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject initStack;
        
        [SerializeField] private List<GameObject> collectableList = new List<GameObject>();
        
        [SerializeField] [Range(0.02f, 1f)] private float lerpDelay;
        
        [SerializeField] private Transform playerTransform;
        
        [SerializeField] private int initSize = 3;

        [SerializeField] private GameObject stackHolder;                          //Levelden Buldur,test amacli boyle kalabilir
        
        

        #endregion

        #endregion

        private void Awake()
        {
            
        }

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
            StackSignals.Instance.onRebuildStack += OnRebuildStack;
            StackSignals.Instance.onChangeColor += OnChangeCollectableColor;
            StackSignals.Instance.onChangeCollectedAnimation += OnChangeCollectedAnimation;
            CoreGameSignals.Instance.onGameOpen+= OnInitializeStack;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack-= OnDecreaseStack;
            StackSignals.Instance.onDecreaseStackOnDroneArea -= OnDecreaseStackOnDroneArea;
            StackSignals.Instance.onRebuildStack -= OnRebuildStack;
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

        private void OnInitializeStack()
        {
            for (int i = 0; i < initSize ; i++)
            {   
                
                var _currentStack = Instantiate(initStack, Vector3.zero, this.transform.rotation);
                
                AddStackOnInitialize(_currentStack);
                
                StackSignals.Instance.onChangeCollectedAnimation?.Invoke(CollectableAnimType.CrouchIdle);
                
            }
        }

        private void OnChangeCollectedAnimation(CollectableAnimType nextCollectableAnimType)
        {
            for (int i = 0; i < collectableList.Count; i++)
            {
                collectableList[i].GetComponent<CollectableManager>().ChangeAnimationOnController(nextCollectableAnimType);
            }
        }
        
        private void OnDoubleStack()
        {
           
        }
        private async void OnChangeCollectableColor(ColorType colorType)
        {   
            for (int i = 0; i < collectableList.Count; i++)
            {
                await Task.Delay(50);
                collectableList[i].GetComponent<CollectableManager>().OnChangeColor(colorType);
            }
        }

        private void AddStackOnInitialize(GameObject currentStack)
        {
            collectableList.Add(currentStack);
            
            currentStack.transform.SetParent(transform);
            
            currentStack.transform.position = collectableList[collectableList.Count-1].transform.position + Vector3.back *1.2f;
        }
        
        private void OnIncreaseStack(GameObject currentStack)
        {
            
            collectableList.Add(currentStack);
            
            currentStack.transform.SetParent(transform);

            currentStack.transform.position = collectableList[collectableList.Count-1].transform.position + Vector3.back *1.2f;

        }

        private void OnDecreaseStack(int currentIndex)
        {
            collectableList.RemoveAt(currentIndex);
            
            collectableList.TrimExcess();
        }
        
        private void OnDecreaseStackOnDroneArea(int currentIndex)
        {
            
            DroneAreaSignals.Instance.onDroneAreaEnter?.Invoke(collectableList[currentIndex].gameObject);

            collectableList.RemoveAt(currentIndex);
            
            collectableList.TrimExcess();

            if (collectableList.Count == 0)
            {
                DroneAreaSignals.Instance.onDroneAreasCollectablesDeath?.Invoke();
            }
            
        }

        private void OnRebuildStack(GameObject currentStack)
        {
            collectableList.Add(currentStack);
            
            playerTransform.position = collectableList[0].transform.position;
            
            CoreGameSignals.Instance.onExitDroneArea?.Invoke();
            
            playerTransform.GetComponent<PlayerManager>().OnStartVerticalMovement();
            
            currentStack.transform.SetParent(transform);
            
            currentStack.transform.position = collectableList[collectableList.Count-1].transform.position + Vector3.back *1.2f;

        }

        
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
        
        
        
        
    }
}
