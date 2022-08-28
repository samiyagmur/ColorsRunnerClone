using Extention;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {

        public UnityAction onIncreaseScore = delegate { };
        public UnityAction onDecreaseScore = delegate { };
        public UnityAction<int>onSendUIScore =delegate { };
        public UnityAction<int> onSendPlayerScore = delegate { };
        public UnityAction<string> onMultiplyAmaunt = delegate { };


    }
}
