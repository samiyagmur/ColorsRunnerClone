using System;
using TMPro;
using Enums;

namespace Datas.ValueObject
{   
    [Serializable]
    public class PublicPlacesData
    {
        public int RequiredBuilds;

        public TextMeshPro RequiredBuildText;
        
        public BuildingState BuildingState = BuildingState.Uncompleted;
        
        public PublicPlacesType PublicPlacesType;
    }
}