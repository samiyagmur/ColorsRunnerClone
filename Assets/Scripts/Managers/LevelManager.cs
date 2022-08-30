using Command;
using Command.IdleLevelCommands;
using Command.LevelCommands;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using System.Threading.Tasks;
using Abstract;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour,ISaveable
    {
       #region Self Variables

        #region Public Variables

        [Header("LevelData")] public LevelData LevelData;
        
        [Header("LevelIdData")] public LevelIdData LevelIdData = new LevelIdData();
        
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
        private int _LevelID;
        private int _IdleLevelId;
        private int _gameScore;
        #endregion

        #endregion

        private void Awake()
        {   
            
            GetData();
            
        }

        private void GetData()
        {
            if (!ES3.FileExists($"Level{_IdleLevelId}.es3"))
            {
                if (!ES3.KeyExists("IdleBuildingDataKey"))
                {   
                    Debug.Log("Key does not exist!");
                    IdleLevelData = GetIdleLevelData();
                    Save(_IdleLevelId);
                }
            }
            Load(LevelIdData.IdleLevelId);
            LevelData = GetLevelData();
        }
        
        private LevelData GetLevelData()
        {
            var newLevelData = LevelIdData.LevelId % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[newLevelData];
        }

        private IdleLevelData GetIdleLevelData()
        {   
            var newIdleLevelData =
                LevelIdData.IdleLevelId % Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList.Count;
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
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeIdleLevel;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveIdleLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onGetIdleLevelID += OnGetIdleLevelId;
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
            _LevelID++;
            Save(_IdleLevelId);
            if (Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_IdleLevelId].IdleLevelState == IdleLevelState.Completed)
            {
                _IdleLevelId++;
                Save(_IdleLevelId);
            }

            CoreGameSignals.Instance.onReset?.Invoke();
            //UISignals.Instance.onChangeLevelText(_LevelID + 1);
            //MoneyPoolManager.Instance.HideAllActiveMoney();
            UISignals.Instance.onSetLevelText?.Invoke(_LevelID);
        }

        private int OnGetLevelId()
        {
            return _LevelID;
        }
        private int OnGetIdleLevelId()
        {
            return _IdleLevelId;
        }
        private void OnReset()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
            CoreGameSignals.Instance.onIdleLevelInitialize?.Invoke();
        }

        private void OnInitializeLevel()

        {
            var newLevelData = _LevelID % Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
            
            levelLoader.InitializeLevel(newLevelData, levelHolder.transform);
        }

        private void OnClearActiveLevel()
        {
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }

        private void OnInitializeIdleLevel()
        {
            var newIdleLevelData =
                _IdleLevelId % Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList.Count;
            
            idleLevelLoader.InitializeIdleLevel(newIdleLevelData,idleLevelHolder.transform);
        }
        
        private void OnClearActiveIdleLevel()
        {
            idleLevelClearer.ClearActiveIdleLevel(idleLevelHolder.transform);
        }

        public void Save(int uniqueId)
        {
            LevelIdData levelIdData= new LevelIdData(_LevelID,_IdleLevelId);
            
            SaveLoadSignals.Instance.onSaveGameData.Invoke(levelIdData,uniqueId);
        }

        public void Load(int uniqueId)
        {
            LevelIdData levelIdData= SaveLoadSignals.Instance.onLoadGameData.Invoke(LevelIdData.LevelKey, uniqueId);
            
            _IdleLevelId = levelIdData.IdleLevelId;
            
            _LevelID = levelIdData.LevelId;
        }
    }
}
