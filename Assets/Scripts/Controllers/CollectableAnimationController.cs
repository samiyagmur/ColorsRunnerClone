using System.Collections;
using UnityEngine;
using Enums;

namespace Controllers
{
    public class CollectableAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator collectabelanimator;

        #endregion
        

        #endregion
        

        public void ChangeAnimationState(CollectableAnimType type)
        {
            collectabelanimator.SetTrigger(type.ToString());
        }
        

    }
}