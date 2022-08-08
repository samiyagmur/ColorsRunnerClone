using System;
using UnityEngine;
using TMPro;
using Enums;

namespace Datas.ValueObject
{   
    [Serializable]
    public class SideObject
    {
        public GameObject Building;

        public TextMeshPro BuildingText;

        public int BuildingMarketPrice;

        public int PayedAmount;

        public int BuildingAdressId;
        
        public BuildingState BuildingState = BuildingState.Uncompleted;

        public BuildingType BuildingType;
    }
}