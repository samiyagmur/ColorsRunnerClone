using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Extention;

namespace Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public UnityAction onVibrateCam = delegate { };
        public UnityAction onVibrateStatus = delegate { };

    }
}