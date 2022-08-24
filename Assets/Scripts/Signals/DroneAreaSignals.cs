using Enums;
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
        
        public UnityAction  onDisableWrongColorGround = delegate {  };
        
        public UnityAction onDroneActive = delegate {  };

        public UnityAction<ColorType> onSetCorrectColorToGround = delegate(ColorType arg0) {  };
        


    }
}