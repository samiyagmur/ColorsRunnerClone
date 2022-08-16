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

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            DroneAreaSignals.Instance.onDisableAllColliders += OndisableallColliders;
        }

        private void UnsubscribeEvents()
        {

            DroneAreaSignals.Instance.onDisableAllColliders -= OndisableallColliders;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        

        private void OndisableallColliders()
        {   
            transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        }
    }
}