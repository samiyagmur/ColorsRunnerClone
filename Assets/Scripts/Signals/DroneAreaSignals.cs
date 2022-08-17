using Extention;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class DroneAreaSignals: MonoSingleton<DroneAreaSignals>
    {
        public UnityAction onDisableAllColliders = delegate {  };
        
        public UnityAction onEnableDroneAreaCollider = delegate {  };
        
        public UnityAction onDisableDroneAreaCollider = delegate {  };
    }
}