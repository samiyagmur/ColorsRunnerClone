using Managers;
using System.Collections;
using UnityEngine;

namespace Controlers
{
    public class PlayerPhysicsController :MonoBehaviour
    {

        [SerializeField]
        private PlayerManager playerManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gate")) playerManager.SendToColorType(other.transform.GetComponent<GateCommand>().ColorType); 

        }

    }
}