using TMPro;
using Enums;

namespace Datas.ValueObject
{
    public class PublicPlacesData
    {
        public int RequiredBuilds;

        public TextMeshPro RequiredBuildText;
        
        public BuildingState BuildingState = BuildingState.Uncompleted;
    }
}