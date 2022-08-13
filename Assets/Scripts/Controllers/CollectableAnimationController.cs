using System.Collections;
using UnityEngine;
using Enums;

namespace Controllers
{
    public class CollectableAnimationController : MonoBehaviour
    {
        [SerializeField] Animator Collectabelanimator;
         
        public void WhenGameOpenStack() { ChangeAnimationState(CollectableAnimType.CrouchIdle);}
        public void WhenPlay() { ChangeAnimationState(CollectableAnimType.Run); }
        public void WhenEnterStack(){ ChangeAnimationState(CollectableAnimType.Run);}
        public void WhenEnterTaretArea() { ChangeAnimationState(CollectableAnimType.CrouchWalk);Debug.Log("turret"); }

        public void WhenExitTaretArea() { ChangeAnimationState(CollectableAnimType.Run); }

        public void WhenEnterDronArea() { ChangeAnimationState(CollectableAnimType.CrouchIdle); }//Gecikmeli

        public void WhenExitDronArea() { ChangeAnimationState(CollectableAnimType.Run); }//bakılcak

        public void WhenCollectableDie() { ChangeAnimationState(CollectableAnimType.Dying);}

        public void ChangeAnimationState(CollectableAnimType type) { Collectabelanimator.SetTrigger(type.ToString()); }
        

    }
}