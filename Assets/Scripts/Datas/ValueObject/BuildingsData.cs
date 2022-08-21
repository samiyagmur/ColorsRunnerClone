using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using TMPro;

namespace Datas.ValueObject
{   
    [Serializable]
    public class BuildingsData
    {
        public bool isDepended;

        public SideObject SideObject;
        
        public int BuildingMarketPrice;

        public int PayedAmount;

        public int BuildingAdressId;
        
        public float Saturation;
        
        public IdleLevelState ıdleLevelState;

    }
}