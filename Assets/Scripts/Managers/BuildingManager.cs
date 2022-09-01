using Controllers;
using Enums;
using Signals;
using System;
using Abstract;
using Controllers.BuildingControllers;
using Datas.UnityObject;
using Datas.ValueObject;
using DG.Tweening;
using UnityEngine;


namespace Managers
{
    public class BuildingManager : MonoBehaviour, ISaveable
    {
        #region Self Variables

        #region Serialized Variables

        public BuildingsData BuildingsData;
        [SerializeField] private BuildingMarketStatusController buildingMarketStatusController;
        [SerializeField] private GameObject sideObject;
        [SerializeField] private BuildingMeshController buildingMeshController;
        [SerializeField] private BuildingPhysicsController buildingPhysicsController;
        [SerializeField] private SideObjectMeshController sideObjectMeshController;
        [SerializeField] private SideObjectMarketStatusController sideObjectMarketStatusController;

        #endregion

        #region Public Variables

        public int BuildingAddressID;


        #endregion

        #region Private Variables

        private int _IdleLevelID;
        private int _SaveIdentifier = 1;
        private string _UniqueIdString;
        private int _uniqueId;

        #endregion

        ScoreZeroStatus scoreZeroStatus;

        #endregion

        

        private BuildingsData GetBuildingsData()
        {
            return Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_IdleLevelID]
                .Buildings[BuildingAddressID];
        }

        private void Awake()
        {
            SetData();
        }

        private void SetData()
        {
            GetIdleLevelID();

            _UniqueIdString = _SaveIdentifier.ToString() + _IdleLevelID.ToString() + BuildingAddressID.ToString();

            int.TryParse(_UniqueIdString,out _uniqueId);
            
            if (!ES3.FileExists($"IdleBuildingDataKey{_uniqueId}.es3"))
            {
                if (!ES3.KeyExists("IdleBuildingDataKey"))
                {
                    BuildingsData = GetBuildingsData();
                    Save(_uniqueId);
                }
            }

            Load(_uniqueId);
            CheckBuildingsScoreStatus(BuildingsData.idleLevelState);

            if (BuildingsData.IsDepended && BuildingsData.idleLevelState == IdleLevelState.Completed)
            {
                CheckSideBuildingsScoreStatus(BuildingsData.SideObject.ıdleLevelState);
            }

            SetDataToControllers();
           
        }


        private void GetIdleLevelID()
        {
            _IdleLevelID = CoreGameSignals.Instance.onGetIdleLevelID.Invoke();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();

        }

        private void SubscribeEvents()
        {

            CoreGameSignals.Instance.onApplicationPause += OnSave;
            CoreGameSignals.Instance.onApplicationQuit += OnSave;
            CoreGameSignals.Instance.onNextLevel += OnSave;
            CoreGameSignals.Instance.onLevelInitialize += OnLoad;
            BuildingSignals.Instance.onActiveTextUpdate += OnActiveTextUpdate;
            BuildingSignals.Instance.onScoreZero += OnScoreZero;


        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onApplicationPause -= OnSave;
            CoreGameSignals.Instance.onApplicationQuit -= OnSave;
            CoreGameSignals.Instance.onNextLevel -= OnSave;
            CoreGameSignals.Instance.onLevelInitialize -= OnLoad;
            BuildingSignals.Instance.onActiveTextUpdate -= OnActiveTextUpdate;
            BuildingSignals.Instance.onScoreZero -= OnScoreZero;
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SetDataToControllers()
        {

            buildingMarketStatusController.UpdatePayedAmountText(BuildingsData.PayedAmount);
            buildingMeshController.Saturation = BuildingsData.Saturation;
            UpdateSaturation();
            if (BuildingsData.IsDepended)
            {
                sideObjectMarketStatusController.UpdatePayedAmountText(BuildingsData.SideObject.PayedAmount);
                sideObjectMeshController.Saturation = BuildingsData.SideObject.Saturation;
                UpDateSideSaturation();
            }

        }

        #region Save-Load

        private void OnSave()
        {
            Save(_uniqueId);
            SetDataToControllers();
        }

        private void OnLoad()
        {
            Load(_uniqueId);
            SetDataToControllers();
        }

        public void Save(int uniqueId)
        {

            BuildingsData = new BuildingsData(BuildingsData.IsDepended,
                BuildingsData.SideObject,
                BuildingAddressID,
                BuildingsData.BuildingMarketPrice,
                BuildingsData.PayedAmount,
                BuildingsData.Saturation,
                BuildingsData.idleLevelState);

            SaveLoadSignals.Instance.onSaveBuildingsData.Invoke(BuildingsData, uniqueId);
        }

        public void Load(int uniqueId)
        {
            BuildingsData _buildingsData =
                SaveLoadSignals.Instance.onLoadBuildingsData.Invoke(BuildingsData.Key, uniqueId);

            BuildingsData.Saturation = _buildingsData.Saturation;
            BuildingsData.PayedAmount = _buildingsData.PayedAmount;
            BuildingsData.idleLevelState = _buildingsData.idleLevelState;
            BuildingsData.BuildingMarketPrice = _buildingsData.BuildingMarketPrice;
            BuildingsData.IsDepended = _buildingsData.IsDepended;
            BuildingsData.SideObject = _buildingsData.SideObject;
        }

        #endregion

        internal void SetScoreStatus()
        {
            scoreZeroStatus = ScoreZeroStatus.Pasive;
        }

        private void OnActiveTextUpdate()
        {
            scoreZeroStatus = ScoreZeroStatus.Active;
        }

        private void OnScoreZero()
        {
            scoreZeroStatus = ScoreZeroStatus.Pasive;
        }
        

        #region UpdateControllers

        public void UpdatePayedAmount()
        {
            BuildingsData.PayedAmount++;
            buildingMarketStatusController.UpdatePayedAmountText(BuildingsData.PayedAmount);
            UpdateSaturation();
        
            if (scoreZeroStatus == ScoreZeroStatus.Active)
            {
                BuildingsData.PayedAmount++;
                buildingMarketStatusController.UpdatePayedAmountText(BuildingsData.PayedAmount);
                UpdateSaturation();
            }
        
                
        }

            public void UpdateSidePayedAmount()
            {
                BuildingsData.SideObject.PayedAmount++;
                sideObjectMarketStatusController.UpdatePayedAmountText(BuildingsData.SideObject.PayedAmount);
                UpDateSideSaturation();
            }

            public void UpDateSideSaturation()
            {
                sideObjectMeshController.CalculateSaturation();
            }

            private void UpdateSaturation()
            {
                buildingMeshController.CalculateSaturation();
            }


            public void UpdateBuildingStatus(IdleLevelState _idleLevelState)
            {
                BuildingsData.idleLevelState = _idleLevelState;
                if (_idleLevelState == IdleLevelState.Completed)
                {
                    buildingPhysicsController.gameObject.SetActive(false);
                    if (!BuildingsData.IsDepended)
                    {
                        BuildingSignals.Instance.onBuildingsCompleted.Invoke(BuildingsData.BuildingAdressId);
                    }
                }
            }

            public void UpdateSideBuildingStatus(IdleLevelState _idleLevelState)
            {
                BuildingsData.SideObject.ıdleLevelState = _idleLevelState;

                BuildingSignals.Instance.onBuildingsCompleted.Invoke(BuildingsData.SideObject.BuildingAdressId);

            }

            public void CheckBuildingsScoreStatus(IdleLevelState _idleLevelState)
            {
                if (_idleLevelState == IdleLevelState.Completed)
                {
                    buildingMarketStatusController.gameObject.SetActive(false);
                }
                else
                {
                    buildingMarketStatusController.gameObject.SetActive(true);
                }
            }

            public void CheckSideBuildingsScoreStatus(IdleLevelState _idleLevelState)
            {
                if (_idleLevelState == IdleLevelState.Completed)
                {
                    sideObjectMarketStatusController.gameObject.SetActive(false);
                }
                else
                {
                    sideObjectMarketStatusController.gameObject.SetActive(true);
                }
            }

            public void OpenSideObject()
            {
                sideObject.SetActive(true);
            }

            #endregion

        }
    }