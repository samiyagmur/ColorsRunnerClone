using System;
using System.Collections.Generic;
using Enums;

namespace Datas.ValueObject
{   
    [Serializable]
    public class IdleLevelData
    {

        public List<BuildingsData> Buildings = new List<BuildingsData>();

        public List<PublicPlacesData> PublicPlacesDatas = new List<PublicPlacesData>();

        public IdleLevelState IdleLevelState;

    }
    
}