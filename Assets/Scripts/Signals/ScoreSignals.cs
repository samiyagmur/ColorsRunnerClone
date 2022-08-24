using Extention;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {

        public UnityAction onIncreaseScore = delegate { };
        public UnityAction onDecreaseScore = delegate { };
        public UnityAction<int>onSendScore =delegate { };


    }
}
