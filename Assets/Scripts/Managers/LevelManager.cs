using Abstract;
using Command.IdleLevelCommands;
using Command.LevelCommands;
using Datas.UnityObject;
using Datas.ValueObject;
using Signals;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour, ISaveable
    {
        #region Self Variables

        #region Public Variables

        [Header("LevelData")] public LevelData LevelData;

        [Header("LevelIdData")] public LevelIdData LevelIdData = new LevelIdData();

        [Header("IdleLevelData")] public IdleLevelData IdleLevelData;

        #endregion Public Variables

        #region Serialized Variables

        [Space][SerializeField] private GameObject levelHolder;
        [Space][SerializeField] private GameObject idleLevelHolder;

        [SerializeField] private LevelLoaderCommand levelLoader;
        [SerializeField] private ClearActiveLevelCommand levelClearer;

        [SerializeField] private IdleLevelLoaderCommand idleLevelLoader;
        [SerializeField] private ClearActiveIdleLevelCommand idleLevelClearer;

        #endregion Serialized Variables

        #region Private Variables

        private int _LevelID;
        private int _IdleLevelId;

        private int _gameScore;
        private int _uniqueID = 1234;

        #endregion Private Variables

        #endregion Self Variables

        #region MonoBehavior Methods

        private void Awake()
        {
            GetData();
        }
        private void Start()
        {
            OnInitializeLevel();
            OnInitializeIdleLevel();
        }

        #endregion

        #region Data Management
        private void GetData()
        {
            if (!ES3.FileExists($"Level{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("Level"))
                {
                    IdleLevelData = GetIdleLevelData();
                    Save(_uniqueID);
                }
            }
            Load(_uniqueID);

            LevelData = GetLevelData();
        }

        private LevelData GetLevelData()
        {
            var newLevelData = _LevelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[newLevelData];
        }

        private IdleLevelData GetIdleLevelData()
        {
            var newIdleLevelData =
                _IdleLevelId % Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList.Count;
            return Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[newIdleLevelData];
        }

        #endregion

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeIdleLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveIdleLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGetIdleLevelID += OnGetIdleLevelId;
            CoreGameSignals.Instance.onIdleLevelChange += OnIncreaseIdleLevel;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            CoreGameSignals.Instance.onApplicationPause += OnSave;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeIdleLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveIdleLevel;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onGetIdleLevelID -= OnGetIdleLevelId;
            CoreGameSignals.Instance.onIdleLevelChange -= OnIncreaseIdleLevel;
            CoreGameSignals.Instance.onApplicationQuit -= OnSave;
            CoreGameSignals.Instance.onApplicationPause -= OnSave;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subscription

        private void OnSave()
        {
            Save(_uniqueID);
        }
        

        #region Level Management
        private async void OnNextLevel()
        {
            _LevelID++;
            Save(_uniqueID);
            await Task.Delay(25);
            CoreGameSignals.Instance.onReset?.Invoke();
            UISignals.Instance.onSetLevelText?.Invoke(_LevelID);
        }

        private void OnIncreaseIdleLevel()
        {
            _IdleLevelId++;
            Save(_uniqueID);
        }

        private int OnGetIdleLevelId()
        {
            return _IdleLevelId;
        }

        private void OnReset()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onClearActiveIdleLevel.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
            CoreGameSignals.Instance.onIdleLevelInitialize?.Invoke();
        }

        private void OnInitializeLevel()

        {
            int newLevelData = _LevelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;

            levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
        }

        private void OnClearActiveLevel()
        {
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }

        private void OnInitializeIdleLevel()
        {
            int newIdleLevelData =
                _IdleLevelId % Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList.Count;

            idleLevelLoader.InitializeIdleLevel(newIdleLevelData, idleLevelHolder.transform);
        }

        private void OnClearActiveIdleLevel()
        {
            idleLevelClearer.ClearActiveIdleLevel(idleLevelHolder.transform);
        } 
        #endregion
       
        #region Level Save and Load
        public void Save(int uniqueId)
        {
            LevelIdData levelIdData = new LevelIdData(_IdleLevelId, _LevelID);

            SaveLoadSignals.Instance.onSaveGameData.Invoke(levelIdData, uniqueId);
        }

        public void Load(int uniqueId)
        {
            LevelIdData levelIdData = SaveLoadSignals.Instance.onLoadGameData.Invoke(LevelIdData.LevelKey, uniqueId);

            _IdleLevelId = levelIdData.IdleLevelId;

            _LevelID = levelIdData.LevelId;
        } 
        #endregion
    }
}