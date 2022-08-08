using Enums;

namespace Command
{
    public class SaveGameCommand
    {
        public void OnSaveGameData(SaveStates state,int levelData)
        {
            if (state == SaveStates.Level)
            {
                ES3.Save("Level", levelData);
            }
        }    
    
    }
}