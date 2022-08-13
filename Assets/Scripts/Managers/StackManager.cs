using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Signals;
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
        
        [SerializeField] private List<GameObject> collectableList = new List<GameObject>();
        [SerializeField] [Range(0.1f, 1f)] private float lerpDelay;
        [SerializeField] private Transform playerTransform;
        

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
            StackSignals.Instance.onChangeColor += OnChangeColor;
        }

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnAddStack;
            StackSignals.Instance.onDecreaseStack-= OnRemoveStack;
            StackSignals.Instance.onChangeColor -= OnChangeColor;
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

        private void OnChangeColor(ColorType colorType)
        {
            for (int i = 0; i < collectableList.Count; i++)
            {
                collectableList[i].GetComponent<CollectableManager>().OnChangeColor(colorType);
            }
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

                collectableList.RemoveAt(currentIndex);
                
                collectableList.TrimExcess();
            }
        }

        private void LerpStackWithMathf()
        {
            for (int i = 0; i < collectableList.Count; i++)
            {

                if (i == 0)
                {
                    var collectablePos = collectableList.ElementAt(i);
                    Vector3 targetPos = playerTransform.position;
                    
                    targetPos = new Vector3(
                        Mathf.Lerp(targetPos.x, collectablePos.transform.position.x, 0.7f),
                        Mathf.Lerp(targetPos.y, collectablePos.transform.position.y, 0.7f),
                        Mathf.Lerp(targetPos.z, collectablePos.transform.position.z - 1.5f, 1));
                }
                else
                {
                    var collectablePos = collectableList.ElementAt(i-1);
                    var targetPos = collectableList.ElementAt(i);
                    targetPos.transform.position = new Vector3(
                        Mathf.Lerp(targetPos.transform.position.x, collectablePos.transform.position.x, 0.7f),
                        Mathf.Lerp(targetPos.transform.position.y, collectablePos.transform.position.y, 0.7f),
                        Mathf.Lerp(targetPos.transform.position.z, collectablePos.transform.position.z - 1.5f, 1));
                }
            }
        }
        
        
    }
}
