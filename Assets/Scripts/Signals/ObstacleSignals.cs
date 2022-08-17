using Extention;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ObstacleSignals: MonoSingleton<ObstacleSignals>
    {
        public UnityAction<Transform> onEnterTurretArea = delegate { };
        public UnityAction onExitTurretArea= delegate { };
    }
}