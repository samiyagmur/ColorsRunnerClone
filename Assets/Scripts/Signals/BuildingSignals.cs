using Extention;
using UnityEngine.Events;

namespace Signals
{
    public class BuildingSignals : MonoSingleton<BuildingSignals>
    {   
        
        public UnityAction onDataReadyToUse = delegate {  };
        public UnityAction<int> onBuildingsCompleted = delegate(int arg0) {  };
    }
}