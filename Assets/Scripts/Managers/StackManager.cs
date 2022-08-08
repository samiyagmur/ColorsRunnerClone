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
        
        [SerializeField] private List<GameObject> cubeList = new List<GameObject>();
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
            StackSignals.Instance.onIncreaseStack += OnRemoveStack;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnAddStack;
            StackSignals.Instance.onIncreaseStack -= OnRemoveStack;
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

            if (cubeList.Count == 0)
            {
                currentStack.transform.localPosition = transform.localPosition;
                cubeList.Add(currentStack);

                return;
            }
            
            currentStack.transform.localPosition = cubeList[cubeList.Count-1].transform.localPosition + Vector3.back * 1.2f;
            
            cubeList.Add(currentStack);
            
        }

        private void OnRemoveStack(GameObject currentStack)
        {
            
        }
        
        private void LerpStack()
        {
            for (int i = 0; i < cubeList.Count; i++)
            {
                if (i == 0)
                {
                    Vector3 xPos = cubeList[i].transform.position;
                    Vector3 targetPos = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y,
                        cubeList[i].transform.position.z);
                    cubeList[i].transform.position = Vector3.Lerp(xPos, targetPos,lerpDelay);
                }
                else
                {
                    Vector3 xPos = cubeList[i].transform.position;
                    Vector3 targetPos = new Vector3(cubeList[i-1].transform.position.x,
                        cubeList[i-1].transform.position.y, cubeList[i].transform.position.z);
                    cubeList[i].transform.position = Vector3.Lerp(xPos, targetPos,lerpDelay);
                }
            }
        }
        
    }
}
