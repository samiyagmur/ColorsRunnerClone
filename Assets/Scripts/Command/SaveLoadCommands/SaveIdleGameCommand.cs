
using Datas.ValueObject;

namespace Command.SaveLoadCommands
{
    public class SaveIdleGameCommand
    {
        public void OnSaveIdleGameData(CityData cityData)
        {
            foreach (var data in cityData.CityList)
            {   
                ES3.Save(data.BuildingAdressId.ToString(),data,"IdleLevelData/IdleLevelData.es3");
            }
        }
    }
}