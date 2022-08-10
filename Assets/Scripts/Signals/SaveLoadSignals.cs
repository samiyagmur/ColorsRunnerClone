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
        public  UnityAction<SaveStates, int> onSaveGameData = delegate { };
        
        public  Func<SaveStates, int> onLoadGameData = delegate { return 0; };
        
        public UnityAction<CityData> onSaveIdleData = delegate(CityData arg0) {  };
        
        //abone olduğum yerde bana ıdledata yollasın
        public Func<SaveStates, CityData> onLoadIdleData;
        
        protected override void Awake()
        {
            base.Awake();
        }
    }
}