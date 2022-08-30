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
        
        public UnityAction<LevelIdData,int> onSaveGameData = delegate { };
        
        public Func<string, int, LevelIdData> onLoadGameData;
        
        public UnityAction<BuildingsData,int> onSaveBuildingsData = delegate { };
        
        public Func<string, int, BuildingsData> onLoadBuildingsData;
        
        public UnityAction<IdleLevelData,int> onSaveIdleData = delegate {  };

        public Func<string, int, IdleLevelData> onLoadIdleData;


    }
}