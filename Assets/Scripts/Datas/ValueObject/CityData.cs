using System;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObject
{   
    [Serializable]
    public class CityData
    {   
        [Space]
        public List<BuildingsData> CityList = new List<BuildingsData>();
        
        [Space]
        public List<PublicPlacesData> PublicPlaces = new List<PublicPlacesData>();
        
        [Space]
        public List<SideObject> SideObjects = new List<SideObject>();
    }
}