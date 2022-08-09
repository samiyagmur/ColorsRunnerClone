using Extention;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        
        public UnityAction<GameObject> onIncreaseStack = delegate(GameObject arg0) {  };
        
        public UnityAction<GameObject> onDecreaseStack = delegate(GameObject arg0) {  };
        protected override void Awake()
        {
            base.Awake();
        }
    }
}
