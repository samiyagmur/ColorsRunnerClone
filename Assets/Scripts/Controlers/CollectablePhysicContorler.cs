using System.Collections;
using UnityEngine;
using Managers;

namespace Controlers
{
    public class CollectablePhysicContorler : MonoBehaviour
    {

        CollectableMenager collectableMenager;
        
        private void OnTriggerEnter(Collider other)
        {

            //if (other.CompareTag("player"))
            //{

            //}
            //if (other.CompareTag("collectable")) collectableMenager.OnIcreaseStack();

           // if (other.CompareTag("obstacle")) collectableMenager.OnDecreaseStack();

            if (other.CompareTag("rainbow")) collectableMenager.OnChangeCollor();

            if (other.CompareTag("color")) collectableMenager.OnChangeCollor();

            if (other.CompareTag("taretArea")) collectableMenager.OnChangeCollor(); 

            if (other.CompareTag("dronArea")) collectableMenager.OnChangeCollor();

           // if (other.CompareTag("nextIdleLevel")) collectableMenager.onHitNextIdleLevel();

        }
        private void OnTriggerStay(Collider other)
        {
            //if (other.CompareTag("buildingTextArea")) collectableMenager.OnDecreaseStack();
        }
    }
}