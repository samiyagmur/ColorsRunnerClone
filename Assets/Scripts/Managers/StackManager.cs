using System;
using System.Collections.Generic;
using Signals;
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
        
        [SerializeField] private List<GameObject> collectableList = new List<GameObject>();
        [SerializeField] [Range(0.1f, 1f)] private float lerpDelay;
        public Transform PlayerTransform;
        

        #endregion

        #endregion
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack += OnAddStack;
            StackSignals.Instance.onDecreaseStack += OnRemoveStack;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnAddStack;
            StackSignals.Instance.onDecreaseStack-= OnRemoveStack;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void FixedUpdate()
        {
            LerpStack();
        }

        private void OnAddStack(GameObject currentStack)
        {   
            currentStack.transform.SetParent(transform);

            if (collectableList.Count == 0)
            {
                currentStack.transform.localPosition = transform.localPosition;
                collectableList.Add(currentStack);

                return;
            }
            
            currentStack.transform.localPosition = collectableList[collectableList.Count-1].transform.localPosition + Vector3.back * 1.2f;
            
            collectableList.Add(currentStack);
            
        }

        private void OnRemoveStack(int currentIndex)
        {
            for (int i = 0; i < collectableList.Count; i++)
            {
                transform.GetChild(currentIndex).SetParent(null);
                
                collectableList.RemoveAt(currentIndex);
                
                collectableList.TrimExcess();
            }
        }
        
        private void LerpStack()
        {
            for (int i = 0; i < collectableList.Count; i++)
            {
                if (i == 0)
                {
                    Vector3 xPos = collectableList[i].transform.position;
                    Vector3 targetPos = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y,
                        collectableList[i].transform.position.z);
                    collectableList[i].transform.position = Vector3.Lerp(xPos, targetPos,lerpDelay);
                }
                else
                {
                    Vector3 xPos = collectableList[i].transform.position;
                    Vector3 targetPos = new Vector3(collectableList[i-1].transform.position.x,
                        collectableList[i-1].transform.position.y, collectableList[i].transform.position.z);
                    collectableList[i].transform.position = Vector3.Lerp(xPos, targetPos,lerpDelay);
                }
            }
        }
        
    }
}
