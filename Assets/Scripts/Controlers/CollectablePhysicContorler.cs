using System.Collections;
using UnityEngine;
using Managers;
using Signals;

namespace Controlers
{
    public class CollectablePhysicContorler : MonoBehaviour
    {

        CollectableMenager collectableMenager;
        
        private void OnTriggerEnter(Collider other)
        {


            if (other.CompareTag("collectable")) collectableMenager.OnIcreaseStack();

            if (other.CompareTag("obstacle")) collectableMenager.OnDecreaseStack();

            if (other.CompareTag("colorGate")) collectableMenager.OnChangeColor(other.gameObject.GetComponent<Renderer>().material);

            if (other.CompareTag("MiniGameGate")) { CoreGameSignals.Instance.onEnterMiniGame?.Invoke(); }

            if (other.CompareTag("dronArea")) collectableMenager.StartPointDronArea();

            if (other.CompareTag("taretArea")) collectableMenager.StartPointTaretArea();

            // if (other.CompareTag("nextIdleLevel")) collectableMenager.onHitNextIdleLevel();
        }
        private void OnTriggerExit(Collider other)
        {
            //if (other.CompareTag("buildingTextArea")) collectableMenager.OnDecreaseStack();
            if (other.CompareTag("dronArea")) collectableMenager.EndPointDronArea();
            if (other.CompareTag("taretArea")) collectableMenager.EndPointTaretArea();
        }
        
    }
}