
using Datas.ValueObject;
using Enums;

namespace Command.SaveLoadCommands
{
    public class SaveIdleGameCommand
    {
        public void Execute(SaveStates saveStates,int idleLevelData)
        {
            
            if (saveStates == SaveStates.IdleLevel)
            {
                ES3.Save("IdleLevel",idleLevelData);
            }
        }
    }
}