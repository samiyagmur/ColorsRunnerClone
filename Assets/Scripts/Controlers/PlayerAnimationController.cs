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

    public void ActivatePlayerAnimation()
    {
        playerAnimator.SetBool("isRunning",true);
    }
    

}
   
}