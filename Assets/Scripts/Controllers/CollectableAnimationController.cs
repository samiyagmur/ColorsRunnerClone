using System.Collections;
using UnityEngine;
using Enums;

namespace Controllers
{
    public class CollectableAnimationController : MonoBehaviour
    {
        [SerializeField] Animator Collectabelanimator;
        
        public void ChangeAnimationState(CollectableAnimType type) { Collectabelanimator.SetTrigger(type.ToString()); }
        

    }
}