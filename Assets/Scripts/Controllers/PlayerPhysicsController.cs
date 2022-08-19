using Managers;
using Command.ObstacleCommands;
using Signals;
using UnityEngine;
using Enums;

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

            if (other.CompareTag("AfterGround"))
            {
                playerManager.StartMovementAfterDroneArea(other.transform);
                playerManager.ChangeForwardSpeeds(ChangeSpeedState.Normal);

            }

            if (other.CompareTag("TurretArea"))
            {
                playerManager.ChangeForwardSpeeds(ChangeSpeedState.EnterTaretArea);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("DroneArea"))
            {
                playerManager.OnStopVerticalMovement();
                playerManager.ChangeForwardSpeeds(ChangeSpeedState.Stop);
            }
            if (other.CompareTag("TurretArea"))
            {
                playerManager.ChangeForwardSpeeds(ChangeSpeedState.Normal);
            }
        }
    }
}