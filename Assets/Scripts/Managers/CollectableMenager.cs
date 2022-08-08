using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Signals;
using Controlers;


namespace Managers
{
    public class CollectableMenager : MonoBehaviour
    {



        #region Self Veriables
        #region SerializeField Veriables
        [SerializeField]
        CollectableMashController collectableMashController;
        [SerializeField]
        CollectableAnimationController collectableAnimationController;
        [SerializeField]
        CollectableParticalController collectableParticalController;

        #endregion

        #endregion


        #region Physical Managment
        //public void OnIcreaseStack() StackSignals.Instance.IncreaseStack?.Invoke();

        //public void OnDecreaseStack() StackSignals.Instance.DecreaseStack?.Invoke();

        //public void onHitRainbow() 

        //public void onHitColor()
        //{

        //}
        public void OnChangeCollor() => collectableMashController.changeCollor();
        

        //public void onHitTaretArea()
        //{

        //}
        //public void onHitDronArea()
        //{

        //}
        //public void onHitBuildingTextArea() => //IdlegameSignals ;

        //public void onHitNextIdleLevel() =>//IdlegameSignals


        #endregion
    }
}