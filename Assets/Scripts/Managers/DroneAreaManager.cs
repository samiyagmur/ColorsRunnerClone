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

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            DroneAreaSignals.Instance.onDisableAllColliders += OnDisableAllColliders;
        }

        private void UnsubscribeEvents()
        {

            DroneAreaSignals.Instance.onDisableAllColliders -= OnDisableAllColliders;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        

        private void OnDisableAllColliders()
        {   
            transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        }
    }
}