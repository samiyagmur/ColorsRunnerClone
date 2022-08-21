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

        public int PayedAmounth;

        public int BuildingAdressId;

        public float Saturation;
        
        public IdleLevelState ıdleLevelState = IdleLevelState.Uncompleted;
        
    }
}