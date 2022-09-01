using Enums;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Animator playerAnimator;

        #endregion Serialized Variables

        #endregion Self Variables





        
        public void ChangeAnimationState(PlayerAnimationType type)
        {
            playerAnimator.Play(type.ToString());
        }
    }
}