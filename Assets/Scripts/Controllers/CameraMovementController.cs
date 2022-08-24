using System.Collections;
using UnityEngine;
using Enums;

namespace Controllers
{
    public class CameraMovementController : MonoBehaviour
    {
        [SerializeField] Animator camAnimator;
        public void whenGameStart() { changeCam(CameraStates.Level); } 

        public void WhenEnterMultiplyArea() { changeCam(CameraStates.MiniGame); }

        public void WhenEnTerIdleArea() { changeCam(CameraStates.Idle); }
        
        public void WhenOnReset() { changeCam(CameraStates.Level); }

        public void changeCam(CameraStates cameraStates)//düzelcek
        {
            switch (cameraStates)
            {
                case CameraStates.Level:
                    camAnimator.Play("Level");
                    break;
                case CameraStates.MiniGame:
                    camAnimator.Play("MiniGame");
                    break;
                case CameraStates.Idle:
                    camAnimator.Play("Idle");
                    break;
            }
        }
    }
}