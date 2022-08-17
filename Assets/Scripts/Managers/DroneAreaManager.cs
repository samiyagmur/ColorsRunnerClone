using System.Threading.Tasks;
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

        [SerializeField] private GameObject droneGameObject;
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
            DroneAreaSignals.Instance.onEnableDroneAreaCollider += OnEnableDroneAreaCollider;
            DroneAreaSignals.Instance.onDisableDroneAreaCollider += OnDisableDroneAreaCollider;
        }

        private void UnsubscribeEvents()
        {

            DroneAreaSignals.Instance.onDisableAllColliders -= OnDisableAllColliders;
            DroneAreaSignals.Instance.onEnableDroneAreaCollider -= OnEnableDroneAreaCollider;
            DroneAreaSignals.Instance.onDisableDroneAreaCollider -= OnDisableDroneAreaCollider;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        

        private void OnDisableAllColliders()
        {
            DroneMovementActive();
            transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(2).GetComponent<BoxCollider>().enabled = false;

        }

        private void OnEnableDroneAreaCollider()
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        private void OnDisableDroneAreaCollider()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        public void DroneMovementActive()
        {
            droneGameObject.SetActive(true);
        }
        
    }
}