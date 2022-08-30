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

        public int CompletedBuildingsCount;
        public IdleLevelData()
        {
            
        }

        public IdleLevelData(IdleLevelState _idleLevelState,int _completedBuildingsCount)
        {
            IdleLevelState = _idleLevelState;
            CompletedBuildingsCount = _completedBuildingsCount;
        }

        public override string GetKey()
        {
            return IdleLevelKey;
        }
    }
    
}