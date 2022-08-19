using Enums;
using Extention;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction onSetStackTarget = delegate {  };
        
        public UnityAction<GameObject> onIncreaseStack = delegate(GameObject arg0) {  };
        
        public UnityAction<int> onDecreaseStack = delegate(int arg0) {  };

        public UnityAction<int> onDecreaseStackOnDroneArea = delegate(int arg0) {  };

        public UnityAction<ColorType> onChangeColor = delegate { };
        
        public UnityAction<CollectableAnimType> onChangeCollectedAnimation = delegate(CollectableAnimType arg0) {  };
        
        public UnityAction onInitializeStack = delegate {  };

        protected override void Awake()
        {
            base.Awake();
        }
    }
}
