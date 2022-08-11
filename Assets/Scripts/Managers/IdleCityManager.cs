using System;
using System.Collections;
using System.Collections.Generic;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class IdleCityManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("BuildingsData")] public CityData CityData;

        public List<GameObject> Buildings = new List<GameObject>();
        
        public List<GameObject> PublicPlaces = new List<GameObject>();
        
        public List<Transform> BuildingsTransforms = new List<Transform>();
        
        public List<Transform> PublicPlacesTransforms = new List<Transform>();

        public List<Transform> SideObjectTransforms = new List<Transform>();

        #endregion

        #region Private Variables

        private int index;

        #endregion

        #region Serialized Variables

        

        #endregion

        #endregion

        private void Awake()
        {
            CityData = GetCityData();
      
        }
        
        private CityData GetCityData() => Resources.Load<CD_Buildings>("Data/CD_Buildings 1").CityData;

        private void Start()
        {
            SaveCityData(CityData);
            
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onApplicationPause += OnSaveCityData;
            CoreGameSignals.Instance.onApplicationQuit += OnSaveCityData;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onApplicationPause -= OnSaveCityData;
            CoreGameSignals.Instance.onApplicationQuit -= OnSaveCityData;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SaveCityData(CityData cityData)
        {
            SaveLoadSignals.Instance.onSaveIdleData?.Invoke(cityData);
        }

        private void OnSaveCityData()
        {
            SaveCityData(CityData);
        }
        
    }
}