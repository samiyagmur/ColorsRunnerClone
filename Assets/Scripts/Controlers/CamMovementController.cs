using System.Collections;
using UnityEngine;
using Enums;

namespace Controlers
{
    public class CamMovementController : MonoBehaviour
    {
        [SerializeField] Animator camAnimator;
        public void whenGameStart() { changeCam(CameraStates.Level); } 

        public void WhenEnterMiniGame() { changeCam(CameraStates.MiniGame); }

        public void WhenEnTerIdleArea() { changeCam(CameraStates.Idle); }
        
        public void WhenOnReset() { changeCam(CameraStates.Level); }

        public void changeCam(CameraStates cameraStates)
        {
            switch (cameraStates)
            {
                case CameraStates.Level:
                    camAnimator.SetTrigger("Level");
                    break;
                case CameraStates.MiniGame:
                    camAnimator.SetTrigger("MiniGame");
                    break;
                case CameraStates.Idle:
                    camAnimator.SetTrigger("Idle");
                    break;

            }
        }
    }
}