using Datas.ValueObject;
using Enums;

namespace Command.SaveLoadCommands
{
    public class SaveIdleLevelProgressCommand
    {
        public void Execute(SaveStates saveStates,IdleLevelData idleLevelData)
        {
            ES3.Save("IdleLevelProgressData",idleLevelData,"IdleLevelData/IdleLevelProgressData.es3");
        }
        
    }
}