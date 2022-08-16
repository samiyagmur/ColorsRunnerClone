using System.Threading.Tasks;
using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class DroneAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion
        #region Private Veriables
        
        #endregion
    
        #region Serialized Variables

        [SerializeField] private GameObject droneAreaCollectableHolder;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            DroneAreaSignals.Instance.onDroneAreaEnter += OnSetDroneAreaHolder;
            
            DroneAreaSignals.Instance.onDroneAreasCollectablesDeath += OnSendCollectablesBackToDeath;
        }

        private void UnsubscribeEvents()
        {
            DroneAreaSignals.Instance.onDroneAreaEnter -= OnSetDroneAreaHolder;
      
            DroneAreaSignals.Instance.onDroneAreasCollectablesDeath -= OnSendCollectablesBackToDeath;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        private void OnSetDroneAreaHolder(GameObject gameObject)
        {
            gameObject.transform.SetParent(droneAreaCollectableHolder.transform);
        }

        private void SendCollectablesBackToStack()
        {
            for (int i = 0; i < droneAreaCollectableHolder.transform.childCount; i++)
            { 
                droneAreaCollectableHolder.transform.GetChild(i).GetComponent<CollectableManager>().IncreaseStackAfterDroneArea(droneAreaCollectableHolder.transform.GetChild(i).gameObject);
            }
        }
        private async void OnSendCollectablesBackToDeath() 
        {
            for (int i = 0; i < droneAreaCollectableHolder.transform.childCount; i++)
            {
                await Task.Delay(50);
                if (droneAreaCollectableHolder.transform.GetChild(i).GetComponent<CollectableManager>().ColorMatchType != ColorMatchType.Match)
                {
                    droneAreaCollectableHolder.transform.GetChild(i).GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimType.Dying);
                    
                    Destroy(droneAreaCollectableHolder.transform.GetChild(i).gameObject,3f);
                }
                
            }

            await Task.Delay(3000);
            SendCollectablesBackToStack();
            transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
           
        }
    }
}