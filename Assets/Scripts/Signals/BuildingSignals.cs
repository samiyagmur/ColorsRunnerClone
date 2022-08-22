using Extention;
using UnityEngine.Events;

namespace Signals
{
    public class BuildingSignals : MonoSingleton<BuildingSignals>
    {   
        
        public UnityAction onDataReadyToUse = delegate {  };
        
        protected override void Awake()
        { 
            base.Awake();
        }
    }
}