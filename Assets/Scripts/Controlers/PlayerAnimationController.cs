using Enums;
using UnityEngine;

namespace Controlers{

public class PlayerAnimationController : MonoBehaviour
{
    #region Self Variables
    #region Serialized Variables

    [SerializeField] private Animator playerAnimator;

    #endregion

    #region Private Variables

    #endregion

    #endregion

    

    public void WhenJoinIdle() {ChangeAnimationState(PlayerAnimationType.Idle);}

    public void WhenRunning() { ChangeAnimationState(PlayerAnimationType.Running); }
    
    public void WhenThrowing() { ChangeAnimationState(PlayerAnimationType.Throw); }
    
    
        
    public void ChangeAnimationState(PlayerAnimationType type)
    {   
        switch (type)
        {   
            case PlayerAnimationType.Idle:
                playerAnimator.SetTrigger("isIdle");
                break;
            case PlayerAnimationType.Running:
                playerAnimator.SetTrigger("isRunning");
                break;
            case PlayerAnimationType.Throw:
                playerAnimator.SetTrigger("isThrow");
                break;
        }
    }
}
   
}