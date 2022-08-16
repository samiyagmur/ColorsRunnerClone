using Extention;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class DroneAreaSignals: MonoSingleton<DroneAreaSignals>
    {
        public UnityAction onDisableAllColliders = delegate {  };
    }
}