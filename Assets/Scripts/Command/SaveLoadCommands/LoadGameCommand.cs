using Datas.ValueObject;
using Enums;

namespace Command.SaveLoadCommands
{
    public class LoadGameCommand
    {
        public int OnLoadGameData(SaveStates currentState)
        {
            if (currentState == SaveStates.Level) return ES3.Load<int>("Level");
            else return 0;
        }

       
    }
}