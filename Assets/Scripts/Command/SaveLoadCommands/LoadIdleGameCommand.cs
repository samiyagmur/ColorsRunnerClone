using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using UnityEngine;

namespace Command.SaveLoadCommands
{
    public class LoadIdleGameCommand
    {
        public CityData OnLoadBuildingsData(SaveStates state)
        {
            if (state == SaveStates.IdleLevel) return ES3.Load<CityData>("CityData");
            
            else return Resources.Load<CD_Buildings>("Data/CD_Buildings").CityData;

        }
    }
}