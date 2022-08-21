using System;
using System.Collections;
using Command;
using Command.SaveLoadCommands;
using Datas.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Private Variables

        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;
        private LoadIdleGameCommand _loadIdleGameCommand;
        private SaveIdleGameCommand _saveIdleGameCommand;

        #endregion

        #region Serialized Variables

        

        #endregion

        #endregion

        private void Awake()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand();
            _loadIdleGameCommand = new LoadIdleGameCommand();
            _saveIdleGameCommand = new SaveIdleGameCommand();

            if (!ES3.FileExists())
            {
                ES3.Save("Level",0,"RunnerLevelData/RunnerLevelData.es3");
            }

            if (!ES3.FileExists())
            {
                ES3.Save("IdleLevel",0,"IdleLevelData/IdleLevelData.es3");
            }
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveRunnerLevelData += _saveGameCommand.OnSaveGameData;
            SaveLoadSignals.Instance.onLoadGameData += _loadGameCommand.OnLoadGameData;
            SaveLoadSignals.Instance.onSaveIdleLevelData += _saveIdleGameCommand.OnSaveIdleGameData;
            SaveLoadSignals.Instance.onLoadIdleData += _loadIdleGameCommand.OnLoadBuildingsData;
        }

        private void UnsubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveRunnerLevelData -= _saveGameCommand.OnSaveGameData;
            SaveLoadSignals.Instance.onLoadGameData -= _loadGameCommand.OnLoadGameData;
            SaveLoadSignals.Instance.onSaveIdleLevelData -= _saveIdleGameCommand.OnSaveIdleGameData;
            SaveLoadSignals.Instance.onLoadIdleData -= _loadIdleGameCommand.OnLoadBuildingsData;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
        
       
    }
}
