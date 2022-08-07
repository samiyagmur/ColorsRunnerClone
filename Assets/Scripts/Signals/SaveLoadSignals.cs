using UnityEngine.Events;
using System;
using Enums;
using Extentions;

namespace Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        public  Action<SaveStates, int> onSaveGameData = delegate { };
        public  Func<SaveStates, int> onLoadGameData = delegate { return 0; };
    }
}