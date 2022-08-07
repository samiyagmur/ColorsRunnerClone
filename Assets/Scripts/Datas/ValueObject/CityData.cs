using System;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObject
{   
    [Serializable]
    public class CityData
    {   
        [Space]
        public List<BuildingsData> CityList;
        
        [Space]
        public List<PublicPlacesData> PublicPlaces;
        
        [Space]
        public List<SideObject> SideObjects;
    }
}