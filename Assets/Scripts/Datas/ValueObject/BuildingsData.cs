﻿using System;
using Enums;
using UnityEngine;
using TMPro;

namespace Datas.ValueObject
{   
    [Serializable]
    public class BuildingsData
    {

        public int BuildingMarketPrice;

        public int PayedAmount;

        public int BuildingAdressId;

        public BuildingState BuildingState = BuildingState.Uncompleted;
        
        public BuildingType BuildingType = BuildingType.Default;

    }
}