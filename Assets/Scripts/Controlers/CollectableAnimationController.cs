using System.Collections;
using UnityEngine;
using Enums;

namespace Controlers
{
    public class CollectableAnimationController : MonoBehaviour
    {
        [SerializeField] Animator animator;

        public void WhenGameOpen() {ChangeAnimationState(CollectableAnimType.Idle);}

        public void WhenPlay() { ChangeAnimationState(CollectableAnimType.Run); }

        public void WhenEnterTaretArea() { ChangeAnimationState(CollectableAnimType.CrouchandWalk); }

        public void WhenExitTaretArea() { ChangeAnimationState(CollectableAnimType.Run); }

        public void WhenEnterDronArea() { ChangeAnimationState(CollectableAnimType.Crouch); }

        public void WhenExitDronArea() { ChangeAnimationState(CollectableAnimType.Run); }

        public void WhenEnterMiniGame() { ChangeAnimationState(CollectableAnimType.Idle); }

        public void WhenEnterIdleArea() { ChangeAnimationState(CollectableAnimType.Run); }

        public void WhenNextLevel() { ChangeAnimationState(CollectableAnimType.Idle); }

        
        public void ChangeAnimationState(CollectableAnimType type)
        {   
            switch (type)
            {   
                case CollectableAnimType.Idle:
                    animator.SetTrigger("Idle");
                    break;
                case CollectableAnimType.Run:
                    animator.SetTrigger("Run");
                    break;
                case CollectableAnimType.Crouch:
                    animator.SetTrigger("Crouch");
                    break;
                case CollectableAnimType.CrouchandWalk:
                    animator.SetTrigger("CrouchandWalk");
                    break;
            }
        }

    }
}