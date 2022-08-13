using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] [Range(0.1f, 1f)] private float lerpDelay;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private int initSize = 3;
        

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
            StackSignals.Instance.onChangeColor += OnChangeColor;
            CoreGameSignals.Instance.onPlay += OnGameStartStack;
            CoreGameSignals.Instance.onGameOpen+= OnInitializeStack;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack-= OnDecreaseStack;
            StackSignals.Instance.onChangeColor -= OnChangeColor;
            CoreGameSignals.Instance.onPlay -= OnGameStartStack;
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
            for (int i = 0; i <initSize ; i++)
            {   
                
                var _currentStack = Instantiate(initStack, Vector3.zero, this.transform.rotation);
                AddStackOnInitialize(_currentStack);
                _currentStack.GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimType.CrouchIdle);
                
            }
        }

        private void OnGameStartStack()
        {
            for (int i = 0; i < collectableList.Count; i++)
            {
                collectableList[i].GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimType.Run);
            }
        }
        private void OnDoubleStack()
        {
           
        }
        private void OnChangeColor(ColorType colorType)
        {
            for (int i = 0; i < collectableList.Count; i++)
            {
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
            
            currentStack.GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimType.Run);
            
        }

        private void OnDecreaseStack(int currentIndex)
        {
            collectableList.RemoveAt(currentIndex);
                
            collectableList.TrimExcess();
        }

        
        private void LerpStackWithMathf()
        {
            for (int i = 0; i < collectableList.Count; i++) 
            {   
                
                 if (i == 0)
                 {  
                     
                     var collectablePos = collectableList.ElementAt(i);
                     Vector3 targetPos = playerTransform.position;
        
                     collectablePos.transform.position= new Vector3(
                         Mathf.Lerp(collectablePos.transform.position.x, targetPos.x, 0.2f),
                         Mathf.Lerp(collectablePos.transform.position.y, targetPos.y, 0.2f),
                         Mathf.Lerp(collectablePos.transform.position.z,  targetPos.z- 1.5f, 1));
                     
                    Quaternion toRotation = Quaternion.LookRotation(targetPos - collectablePos.transform.position);
                    toRotation = Quaternion.Euler(0,toRotation.eulerAngles.y,0);
                    collectablePos.transform.rotation = Quaternion.Slerp(collectablePos.transform.rotation,toRotation,1f);
                 }
                 else
                 {
                     var collectablePos = collectableList.ElementAt(i-1);
                     var targetPos = collectableList.ElementAt(i);
                     targetPos.transform.position = new Vector3(
                         Mathf.Lerp(targetPos.transform.position.x, collectablePos.transform.position.x, 0.2f),
                         Mathf.Lerp(targetPos.transform.position.y, collectablePos.transform.position.y, 0.2f),
                         Mathf.Lerp(targetPos.transform.position.z, collectablePos.transform.position.z - 1.5f, 1));
                     
                     Quaternion toRotation = Quaternion.LookRotation(collectablePos.transform.position - targetPos.transform.position);
                     toRotation = Quaternion.Euler(0,toRotation.eulerAngles.y,0);
                     targetPos.transform.rotation = Quaternion.Slerp(collectablePos.transform.rotation,toRotation,1f);

                 }
            }
        }
        
        
        
        
    }
}
