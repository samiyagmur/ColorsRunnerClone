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
        
        public UnityAction<SaveStates,int> onSaveIdleLevelData = delegate{  };
        
        public UnityAction<LevelIdData,int> onSaveGameData = delegate { };
        
        public UnityAction<BuildingsData,int> onSaveIdleData = delegate { };

        public Func<string, int, LevelIdData> onLoadGameData;
        
        public Func<string, int, BuildingsData> onLoadBuildingsData;


    }
}