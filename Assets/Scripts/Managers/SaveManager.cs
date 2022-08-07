using Command;
using Command.SaveLoadCommands;
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

        #endregion

        #region Serialized Variables

        

        #endregion

        #endregion

        private void Awake()
        {
            loadGameCommand = new LoadGameCommand();
            saveGameCommand = new SaveGameCommand();

            if (!ES3.FileExists())
            {
                ES3.Save("Score", 0);
                ES3.Save("Level",0);
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
        }

        private void UnsubscribeEvents()
        {
            SaveLoadSignals.Instance.onSaveGameData -= saveGameCommand.OnSaveGameData;
            SaveLoadSignals.Instance.onLoadGameData -= loadGameCommand.OnLoadGameData;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
    }
}
