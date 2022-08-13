using System.Collections;
using UnityEngine;
using Enums;

namespace Controlers
{
    public class CollectableAnimationController : MonoBehaviour
    {
        [SerializeField] Animator Collectabelanimator;
         
        public void WhenGameOpenUnStack() { ChangeAnimationState(CollectableAnimType.Idle);}

        public void WhenPlay() { ChangeAnimationState(CollectableAnimType.Run); }

        public void WhenEnterTaretArea() { ChangeAnimationState(CollectableAnimType.CrouchWalk);Debug.Log("turret"); }

        public void WhenExitTaretArea() { ChangeAnimationState(CollectableAnimType.Run); }

        public void WhenEnterDronArea() { ChangeAnimationState(CollectableAnimType.CrouchIdle); }//Gecikmeli

        public void WhenExitDronArea() { ChangeAnimationState(CollectableAnimType.Run); }//bakılcak

        public void WhenCollectableDie() { ChangeAnimationState(CollectableAnimType.Dying);}

        public void ChangeAnimationState(CollectableAnimType type) { Collectabelanimator.SetTrigger(type.ToString()); }
        

    }
}