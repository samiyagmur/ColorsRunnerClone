using Command.ObstacleCommands;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private PlayerManager playerManager;
        private float _timer = 0f;
        #endregion Serialized Variables

        #endregion Self Variables

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Gate") || other.CompareTag("Rainbow"))
                playerManager.SendToColorType(other.transform.GetComponent<GateController>().colorType);

            if (other.CompareTag("Rainbow")) playerManager.IsHitRainbow();

            if (other.CompareTag("Collectable")) playerManager.IsHitCollectable();

            else if (other.CompareTag("DroneArea"))
            {
                playerManager.DeActivateScore(false);
            }
            else if (other.CompareTag("AfterGround"))
            {
                playerManager.StartMovementAfterDroneArea(other.transform);
                playerManager.ChangeForwardSpeeds(ChangeSpeedState.Normal);
                playerManager.DeActivateScore(true);
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
            if (other.CompareTag("PaymentArea"))
            {
                playerManager.ExitPaymentArea();
               
            }
        }

        private void OnTriggerStay(Collider other)
        {
            

            if (other.CompareTag("PaymentArea"))
            {
                _timer -= Time.fixedDeltaTime;
                if (_timer <= 0)
                {
                    _timer = 0.2f;
                    

                    if (playerManager.ScoreVaryant>0)
                    {
                        playerManager.SetupScore();


                    }
                    else
                    {
                        playerManager.PlayerPhysicDisabled();
                    }
                }
            }
        }
    }
}