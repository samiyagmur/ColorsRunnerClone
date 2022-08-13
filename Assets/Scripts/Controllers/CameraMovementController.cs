using System.Collections;
using UnityEngine;
using Enums;

namespace Controllers
{
    public class CameraMovementController : MonoBehaviour
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
                    camAnimator.Play("Level");
                    Debug.Log("level");
                    break;
                case CameraStates.MiniGame:
                    camAnimator.Play("MiniGame");
                    Debug.Log("mini");
                    break;
                case CameraStates.Idle:
                    camAnimator.Play("Idle");
                    Debug.Log("idle");
                    break;
                

            }
        }
    }
}