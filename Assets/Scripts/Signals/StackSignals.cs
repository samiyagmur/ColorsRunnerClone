using Extention;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        
        public UnityAction<GameObject> onIncreaseStack = delegate(GameObject arg0) {  };
        
        public UnityAction<int> onDecreaseStack = delegate(int arg0) {  };

        public UnityAction<Material> onMaterialChange = delegate{ };

        public UnityAction<Material> onMaterialChangeForDroneArea = delegate { };
        protected override void Awake()
        {
            base.Awake();
        }
    }
}
