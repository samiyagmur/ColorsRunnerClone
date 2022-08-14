using System;
using Managers;
using System.Collections;
using Command.ObstacleCommands;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController :MonoBehaviour
    {

        [SerializeField]
        private PlayerManager playerManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gate")) playerManager.SendToColorType(other.transform.GetComponent<GateController>().colorType);

            if (other.CompareTag("DroneArea"))
            {
                CoreGameSignals.Instance.onEnterDroneArea?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("DroneArea"))
            {
                playerManager.OnStopVerticalMovement();
            }
        }
    }
}