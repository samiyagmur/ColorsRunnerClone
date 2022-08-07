using System;
using System.Collections;
using System.Collections.Generic;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
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
            StartCoroutine(SaveData());
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
      
        }

        private void UnsubscribeEvents()
        {
        
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator SaveData()
        {   
            yield return new WaitForSeconds(1f);
            foreach (var data in CityData.CityList)
            {
                ES3.Save(data.BuildingAdressId.ToString(),data);
            }
        }
    }
}