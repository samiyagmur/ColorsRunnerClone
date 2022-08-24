using Enums;

namespace Command.SaveLoadCommands
{
    public class SaveGameCommand
    {
        public void Execute(SaveStates state,int levelData)
        {
            if (state == SaveStates.Level)
            {
                ES3.Save("Level", levelData);
            }
        }    
    
    }
}