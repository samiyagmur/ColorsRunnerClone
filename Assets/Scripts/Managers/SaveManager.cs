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
        private LoadIdleGameCommand _loadIdleGameCommand;
        private SaveIdleGameCommand _saveIdleGameCommand;
        private SaveIdleLevelProgressCommand _saveIdleLevelProgressCommand;
        private LoadIdleLevelProgressCommand _loadIdleLevelProgressCommand;

        private IdleLevelData _idleLevelData;

        #endregion
        
        #endregion

        private void Awake()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand();
            _loadIdleGameCommand = new LoadIdleGameCommand();
            _saveIdleGameCommand = new SaveIdleGameCommand();
            _saveIdleLevelProgressCommand = new SaveIdleLevelProgressCommand();
            _loadIdleLevelProgressCommand = new LoadIdleLevelProgressCommand();
            if (!ES3.FileExists())
            {
                ES3.Save("Level",0,"RunnerLevelData/RunnerLevelData.es3");
            }

            if (!ES3.FileExists())
            {
                ES3.Save("IdleLevel",0,"IdleLevelData/IdleLevelData.es3");
            }

            if (!ES3.FileExists())
            {
                ES3.Save("IdleLevelProgress","IdleLevelProgress/IdleLevelProgressData.es3");
            }
        }
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveRunnerLevelData += _saveGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadGameData += _loadGameCommand.Execute;
            SaveLoadSignals.Instance.onSaveIdleLevelData += _saveIdleGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadIdleData += _loadIdleGameCommand.Execute;
            SaveLoadSignals.Instance.onSaveIdleLevelProgressData += _saveIdleLevelProgressCommand.Execute;
            SaveLoadSignals.Instance.onLoadIdleLevelProgressData += _loadIdleLevelProgressCommand.Execute;
        }

        private void UnsubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveRunnerLevelData -= _saveGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadGameData -= _loadGameCommand.Execute;
            SaveLoadSignals.Instance.onSaveIdleLevelData -= _saveIdleGameCommand.Execute;
            SaveLoadSignals.Instance.onLoadIdleData -= _loadIdleGameCommand.Execute;
            SaveLoadSignals.Instance.onSaveIdleLevelProgressData -= _saveIdleLevelProgressCommand.Execute;
            SaveLoadSignals.Instance.onLoadIdleLevelProgressData -= _loadIdleLevelProgressCommand.Execute;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
    }
}
