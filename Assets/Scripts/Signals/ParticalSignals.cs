using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Extention;


namespace Signals
{
    public class ParticalSignals : MonoSingleton<ParticalSignals>
    {
        public UnityAction<Vector3> onParticleBurst = delegate { };
        public UnityAction<Vector3> onCollectableDead = delegate { };
        public UnityAction onParticleStop = delegate { };

    }
}