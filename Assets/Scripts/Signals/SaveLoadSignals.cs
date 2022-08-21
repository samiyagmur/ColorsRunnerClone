using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Datas.ValueObject;
using Enums;
using Extention;

namespace Signals
{
    public class SaveLoadSignals : MonoSingleton<SaveLoadSignals>
    {
        public UnityAction<SaveStates, int> onSaveRunnerLevelData = delegate { };
        
        public Func<SaveStates, int> onLoadGameData = delegate { return 0; };
        
        public UnityAction<SaveStates,int> onSaveIdleLevelData = delegate{  };
        
        public UnityAction<SaveStates,IdleLevelData> onSaveIdleLevelProgressData = delegate{  };
        
        public Func<SaveStates,IdleLevelData,IdleLevelData> onLoadIdleLevelProgressData = delegate { return new IdleLevelData();};
        
        public Func<SaveStates, int> onLoadIdleData = delegate{ return 0;};
        
        protected override void Awake()
        {
            base.Awake();
        }
    }
}