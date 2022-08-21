
using Datas.ValueObject;
using Enums;

namespace Command.SaveLoadCommands
{
    public class SaveIdleGameCommand
    {
        public void OnSaveIdleGameData(SaveStates saveStates,int idleLevelData)
        {
            // foreach (var data in ıdleLevelData)
            // {   
            //     ES3.Save(data.BuildingAdressId.ToString(),data,"IdleLevelData/IdleLevelData.es3");
            // }
            if (saveStates == SaveStates.IdleLevel)
            {
                ES3.Save("IdleLevel",idleLevelData);
            }
        }
    }
}