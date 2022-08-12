using System.Collections;
using UnityEngine;
using Managers;
using Signals;

namespace Controlers
{
    public class CollectablePhysicController : MonoBehaviour
    {

        [SerializeField]
        private CollectableManager collectableManager;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable")) collectableManager.OnIcreaseStack();

            if (other.CompareTag("Obstacle")) collectableManager.OnDecreaseStack();

            //if (other.CompareTag("MiniGameGate")) { CoreGameSignals.Instance.onEnterMiniGame?.Invoke(); }


            //if (other.CompareTag("DroneArea")) collectableManager.StartPointDroneArea(other.gameObject.GetComponent<Renderer>().material);

            if (other.CompareTag("TurretArea")) collectableManager.StartPointTurretArea();

            // if (other.CompareTag("nextIdleLevel")) collectableMenager.onHitNextIdleLevel();
        }
        private void OnTriggerExit(Collider other)
        {
            //if (other.CompareTag("buildingTextArea")) collectableMenager.OnDecreaseStack();
            if (other.CompareTag("DroneArea")) collectableManager.EndPointDronArea();
            if (other.CompareTag("TurretArea")) collectableManager.EndPointTaretArea();
        }
        
    }
}