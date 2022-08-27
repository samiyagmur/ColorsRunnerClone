using Command;
using Command.IdleLevelCommands;
using Command.LevelCommands;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
       #region Self Variables

        #region Public Variables

        [Header("LevelData")] public LevelData LevelData;
        [Header("IdleLevelData")] public IdleLevelData IdleLevelData;

        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private GameObject levelHolder;
        [Space] [SerializeField] private GameObject idleLevelHolder;
        
        [SerializeField] private LevelLoaderCommand levelLoader;
        [SerializeField] private ClearActiveLevelCommand levelClearer;

        [SerializeField] private IdleLevelLoaderCommand idleLevelLoader;
        [SerializeField] private ClearActiveIdleLevelCommand idleLevelClearer;

        #endregion

        #region Private Variables
        private int _levelID;
        private int _idleLevelID;
        private int _gameScore;
        #endregion

        #endregion

        private void Awake()
        {   
            
            _levelID = GetActiveLevel();
            _idleLevelID = GetActiveIdleLevel();
            LevelData = GetLevelData();
            IdleLevelData = GetIdleLevelData();
        }
        

        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }

        private int GetActiveIdleLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("IdleLevel") ? ES3.Load<int>("IdleLevel") : 0;
        }

        private LevelData GetLevelData()
        {
            var newLevelData = _levelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[newLevelData];
        }

        private IdleLevelData GetIdleLevelData()
        {   
            var newIdleLevelData =
                _idleLevelID % Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList.Count;
            return Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[newIdleLevelData];
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;
            CoreGameSignals.Instance.onGetIdleLevelID += OnGetIdleLevelID;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onGetLevelID -= OnGetLevelID;
            CoreGameSignals.Instance.onGetIdleLevelID -= OnGetIdleLevelID;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            OnInitializeLevel();
            OnInitializeIdleLevel();
        }

        private void OnNextLevel()
        {
            _levelID++;
            Debug.Log(_levelID);
            if (Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_idleLevelID].IdleLevelState == IdleLevelState.Completed)
            {
                _idleLevelID++;
               
            }
            CoreGameSignals.Instance.onReset?.Invoke();
            //UISignals.Instance.onChangeLevelText(_levelID + 1);
            //MoneyPoolManager.Instance.HideAllActiveMoney();
        }

  
        private void OnReset()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            SaveLoadSignals.Instance.onSaveRunnerLevelData?.Invoke(SaveStates.Level, _levelID);
            SaveLoadSignals.Instance.onSaveIdleLevelData?.Invoke(SaveStates.IdleLevel, _idleLevelID);
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
            CoreGameSignals.Instance.onIdleLevelInitialize?.Invoke();
        }

        private int OnGetLevelID()
        {
            return _levelID;
        }

        private int OnGetIdleLevelID()
        {
            return _idleLevelID;
        }
        private void OnInitializeLevel()
        {
            var newLevelData = _levelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
            levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
        }

        private void OnClearActiveLevel()
        {
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }

        private void OnInitializeIdleLevel()
        {
            var newIdleLevelData =
                _idleLevelID % Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList.Count;
            idleLevelLoader.InitializeIdleLevel(newIdleLevelData,idleLevelHolder.transform);
        }
        
        private void OnClearActiveIdleLevel()
        {
            idleLevelClearer.ClearActiveIdleLevel(idleLevelHolder.transform);
        }
    }
}
