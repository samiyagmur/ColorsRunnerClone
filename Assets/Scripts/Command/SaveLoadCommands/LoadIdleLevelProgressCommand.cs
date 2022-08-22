using Datas.ValueObject;
using Enums;
using ToonyColorsPro.ShaderGenerator;

namespace Command.SaveLoadCommands
{
    public class LoadIdleLevelProgressCommand
    {
        public IdleLevelData Execute(SaveStates saveStates,IdleLevelData idleLevelData)
        {
            return ES3.Load<IdleLevelData>("IdleLevelProgressData","IdleLevelData/IdleLevelProgressData.es3");
            
        }
    }
}