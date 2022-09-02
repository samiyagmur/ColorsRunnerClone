using Enums;
using Extention;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {

      
        public UnityAction<ScoreStatus> onUpdateScore = delegate { };
        public UnityAction<int>onSendUIScore =delegate { };
        public UnityAction <int> onSendPlayerScore = delegate { };
        public UnityAction<string> onMultiplyAmaunt = delegate { };
       
        
        


    }
}
