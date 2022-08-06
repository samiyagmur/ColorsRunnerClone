using System;
using System.Collections.Generic;

namespace Datas.ValueObject
{   
    [Serializable]
    public class CityData
    {
        public List<BuildingsData> CityList;

        public List<PublicPlacesData> PublicPlaces;

        public List<SideObject> SideObjects;
    }
}