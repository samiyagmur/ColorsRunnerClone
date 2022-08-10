using System.Collections;
using UnityEngine;
using Managers;
using Signals;

namespace Controlers
{
    public class CollectablePhysicContorler : MonoBehaviour
    {

        [SerializeField]
        private CollectableMenager collectableMenager;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable")) collectableMenager.OnIcreaseStack();

            if (other.CompareTag("Obstacle")) collectableMenager.OnDecreaseStack();

            if (other.CompareTag("ColorGate")) collectableMenager.OnChangeColor(other.gameObject.GetComponent<Renderer>().material);

            //if (other.CompareTag("MiniGameGate")) { CoreGameSignals.Instance.onEnterMiniGame?.Invoke(); }

            if (other.CompareTag("DroneArea")) collectableMenager.StartPointDroneArea(other.gameObject.GetComponent<Renderer>().material);

            if (other.CompareTag("TurretArea")) collectableMenager.StartPointTurretArea();

            // if (other.CompareTag("nextIdleLevel")) collectableMenager.onHitNextIdleLevel();
        }
        private void OnTriggerExit(Collider other)
        {
            //if (other.CompareTag("buildingTextArea")) collectableMenager.OnDecreaseStack();
            if (other.CompareTag("DroneArea")) collectableMenager.EndPointDronArea();
            if (other.CompareTag("TurretArea")) collectableMenager.EndPointTaretArea();
        }
        
    }
}