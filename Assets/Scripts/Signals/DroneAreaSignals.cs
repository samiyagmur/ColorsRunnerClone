using Extention;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class DroneAreaSignals: MonoSingleton<DroneAreaSignals>
    {   
        public UnityAction<GameObject> onDroneAreaEnter = delegate(GameObject arg0) {  }; 
        
        public UnityAction onDroneAreasCollectablesDeath= delegate() {  }; 
        
        public UnityAction<GameObject> onRebuildStack = delegate(GameObject arg0) {  }; 
    }
}