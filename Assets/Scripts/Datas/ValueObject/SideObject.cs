using System;
using UnityEngine;
using TMPro;
using Enums;

namespace Datas.ValueObject
{   
    [Serializable]
    public class SideObject
    {

        public int BuildingMarketPrice;

        public int PayedAmount;

        public int BuildingAdressId;

        public float Saturation;
        
        public IdleLevelState ıdleLevelState = IdleLevelState.Uncompleted;
        
    }
}