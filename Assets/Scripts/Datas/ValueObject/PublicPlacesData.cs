using System;
using TMPro;
using Enums;

namespace Datas.ValueObject
{   
    [Serializable]
    public class PublicPlacesData
    {
        public int RequiredBuilds;

        public int PayedAmound;
        
        //public TextMeshPro RequiredBuildText;
        
        public IdleLevelState ıdleLevelState = IdleLevelState.Uncompleted;
        
        public PublicPlacesType PublicPlacesType;
    }
}