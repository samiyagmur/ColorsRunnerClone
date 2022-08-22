using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using UnityEngine;

namespace Command.SaveLoadCommands
{
    public class LoadIdleGameCommand
    {
        public int OnLoadBuildingsData(SaveStates state)
        {
            if (state == SaveStates.IdleLevel) return ES3.Load<int>("IdleLevel");
            else return 0;
        }
    }
}