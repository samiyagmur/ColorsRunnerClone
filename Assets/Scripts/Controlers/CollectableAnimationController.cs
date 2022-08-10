using System.Collections;
using UnityEngine;
using Enums;

namespace Controlers
{
    public class CollectableAnimationController : MonoBehaviour
    {
        [SerializeField] Animator Collectabelanimator;

        public void WhenGameOpen() {ChangeAnimationState(CollectableAnimType.Crouch);}

        public void WhenPlay() { ChangeAnimationState(CollectableAnimType.Run); }

        public void WhenEnterTaretArea() { ChangeAnimationState(CollectableAnimType.CrouchandWalk); }

        public void WhenExitTaretArea() { ChangeAnimationState(CollectableAnimType.Run); }

        public void WhenEnterDronArea() { ChangeAnimationState(CollectableAnimType.Crouch); }//Gecikmeli

        public void WhenExitDronArea() { ChangeAnimationState(CollectableAnimType.Run); }//bakılcak


        public void WhenEnterMiniGame() { ChangeAnimationState(CollectableAnimType.Idle); }//durma 

        //public void WhenEnterIdleArea() { ChangeAnimationState(CollectableAnimType.Run); }//

        public void WhenNextLevel() { ChangeAnimationState(CollectableAnimType.Crouch); }//

       // public void WhenEnterTextArea() { ChangeAnimationState(CollectableAnimType.openArms); }

        
        public void ChangeAnimationState(CollectableAnimType type)
        {   
            switch (type)
            {   
                case CollectableAnimType.Idle:
                    Collectabelanimator.SetTrigger("Idle");
                    break;
                case CollectableAnimType.Run:
                    Collectabelanimator.SetTrigger("Run");
                    break;
                case CollectableAnimType.Crouch:
                    Collectabelanimator.SetTrigger("Crouch");
                    break;
                case CollectableAnimType.CrouchandWalk:
                    Collectabelanimator.SetTrigger("CrouchandWalk");
                    break;
            }
        }

    }
}