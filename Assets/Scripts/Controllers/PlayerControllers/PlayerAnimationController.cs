using System;
using Enums;
using UnityEngine;

namespace Controllers{

public class PlayerAnimationController : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables

    [SerializeField] private Animator playerAnimator;

    #endregion

    #region Private Variables

    #endregion

    #endregion
    

    public void WhenThrowing() { ChangeAnimationState(PlayerAnimationType.Throw); }

    public void ChangeAnimationState(PlayerAnimationType type)
    {
            playerAnimator.Play(type.ToString());
    }
}
   
}