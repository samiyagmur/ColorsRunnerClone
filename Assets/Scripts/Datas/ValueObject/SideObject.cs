using System;
using UnityEngine;
using TMPro;
using Enums;

namespace Datas.ValueObject
{   
    [Serializable]
    public class SideObject
    {
        //public TextMeshPro BuildingText;

        public int BuildingMarketPrice;

        public int PayedAmount;

        public int BuildingAdressId;

        public int RequiredBuildingAdressId;
        
        public IdleLevelState ıdleLevelState = IdleLevelState.Uncompleted;

        public BuildingType BuildingType;
    }
}