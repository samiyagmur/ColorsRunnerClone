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
    



    public void ChangeAnimationState(PlayerAnimationType type)
    {
            //ınvoke
            //when player change animation type,this methost will work.
            
           playerAnimator.Play(type.ToString());

            //switch (type)
            //{
            //    case PlayerAnimationType.Idle:
            //        playerAnimator.Play("Idle");
            //        break;
            //    case PlayerAnimationType.Running:
            //        playerAnimator.Play("Running");
            //        break;
            //    case PlayerAnimationType.Throw:
            //        playerAnimator.Play("Throw");
            //        break;
            //    case PlayerAnimationType.Next:
            //        break;
                
                    
            //}


        }
    }

}