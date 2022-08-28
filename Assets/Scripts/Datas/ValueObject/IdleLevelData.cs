using System;
using System.Collections.Generic;
using Abstract;
using Enums;

namespace Datas.ValueObject
{   
    [Serializable]
    public class IdleLevelData : SaveableEntity
    {
        public string IdleLevelKey = "IdleLevelDataKey";

        public List<BuildingsData> Buildings;

        public IdleLevelState IdleLevelState;

        public IdleLevelData()
        {
            
        }

        public IdleLevelData(IdleLevelState idleLevelState)
        {
            IdleLevelState = idleLevelState;
        }
        
        public override string GetKey()
        {
            return IdleLevelKey;
        }
    }
    
}