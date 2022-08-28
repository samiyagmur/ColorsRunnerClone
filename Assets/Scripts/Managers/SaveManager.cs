using System;
using System.Collections;
using Command;
using Command.SaveLoadCommands;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;
 

        #endregion
        
        #endregion

        private void Awake()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand();
         
        }
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveGameData += _saveGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadGameData += _loadGameCommand.Execute<LevelIdData>;
            SaveLoadSignals.Instance.onSaveIdleData += _saveGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadBuildingsData += _loadGameCommand.Execute<BuildingsData>;
        }

        private void UnsubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveGameData -= _saveGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadGameData -= _loadGameCommand.Execute<LevelIdData>;
            SaveLoadSignals.Instance.onSaveIdleData-= _saveGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadBuildingsData -= _loadGameCommand.Execute<BuildingsData>;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
    }
}
