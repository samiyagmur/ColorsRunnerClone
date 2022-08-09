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

        private LoadGameCommand loadGameCommand;
        private SaveGameCommand saveGameCommand;
        private LoadIdleGameCommand loadIdleGameCommand;
        private SaveIdleGameCommand saveIdleGameCommand;

        #endregion

        #region Serialized Variables

        

        #endregion

        #endregion

        private void Awake()
        {
            loadGameCommand = new LoadGameCommand();
            saveGameCommand = new SaveGameCommand();
            loadIdleGameCommand = new LoadIdleGameCommand();
            saveIdleGameCommand = new SaveIdleGameCommand();

            if (!ES3.FileExists())
            {
                ES3.Save("Level",0,"RunnerLevelData/RunnerLevelData.es3");
            }
        }

        private void Start()
        {
            if (!ES3.FileExists())
            {
                
            }
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveGameData += saveGameCommand.OnSaveGameData;
            SaveLoadSignals.Instance.onLoadGameData += loadGameCommand.OnLoadGameData;
            SaveLoadSignals.Instance.onSaveIdleData += saveIdleGameCommand.OnSaveIdleGameData;
            //SaveLoadSignals.Instance.onLoadIdleData += loadIdleGameCommand.OnLoadBuildingsData;
        }

        private void UnsubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveGameData -= saveGameCommand.OnSaveGameData;
            SaveLoadSignals.Instance.onLoadGameData -= loadGameCommand.OnLoadGameData;
            SaveLoadSignals.Instance.onSaveIdleData -= saveIdleGameCommand.OnSaveIdleGameData;
            //SaveLoadSignals.Instance.onLoadIdleData -= loadIdleGameCommand.OnLoadBuildingsData;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
        
       
    }
}
