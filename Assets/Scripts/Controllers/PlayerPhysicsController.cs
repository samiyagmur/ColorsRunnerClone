using Managers;
using Command.ObstacleCommands;
using Signals;
using UnityEngine;
using Enums;

namespace Controllers
{
    public class PlayerPhysicsController :MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField]
        private PlayerManager playerManager;
        

        #endregion

        #endregion
       

      
        private void OnTriggerEnter(Collider other)
        {   
            if (other.CompareTag("Gate")|| other.CompareTag("Rainbow"))
                playerManager.SendToColorType(other.transform.GetComponent<GateController>().colorType);
            if (other.CompareTag("Rainbow")) playerManager.IsHitRainbow();

            if (other.CompareTag("Collectable")) playerManager.IsHitCollectable();

            else if (other.CompareTag("DroneArea"))
            {
               // CoreGameSignals.Instance.onEnterDroneArea?.Invoke();
            }

            else if (other.CompareTag("AfterGround"))
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
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("PaymentArea")) playerManager.IsEnterPaymentArea();

        }

    }
}