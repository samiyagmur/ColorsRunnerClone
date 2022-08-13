using Enums;
using Extention;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        
        public UnityAction<GameObject> onIncreaseStack = delegate(GameObject arg0) {  };
        

        public UnityAction<int> onDecreaseStack = delegate(int arg0) {  };

        public UnityAction<ColorType> onChangeColor = delegate { };

        protected override void Awake()
        {
            base.Awake();
        }
    }
}
