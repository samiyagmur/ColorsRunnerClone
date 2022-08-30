using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Extention
{
    
    public class Pool : MonoSingleton<Pool>
    {
        private PoolData Data;
        [Space]
        [SerializeField] private List<PoolData> items;
        [Space]
        [SerializeField] private List<GameObject> pooledİtems=new List<GameObject>();
        private void Awake()
        {
            Data = GetPoolData();
        }

        private PoolData GetPoolData()
        {   
            return Resources.Load<CD_PoolData>("Data/CD_PoolData").poolData;
        }

        private void Start()
        {
            SetPool();
        }

        private void SetPool()
        {
            foreach (PoolData item in items)
            {
                for (int i = 0; i < item.Amount; i++)
                {
                    GameObject pooledObj = Instantiate(item.CollectablePrefab);
                    pooledObj.SetActive(false);
                    pooledObj.transform.SetParent(transform);
                    pooledİtems.Add(pooledObj);

                }

            }
        }

        public GameObject GetPooledItem(string tag)
        {
            for (int i = 0; i < pooledİtems.Count; i++)
            {
                if (!pooledİtems[i].activeInHierarchy && pooledİtems[i].CompareTag(tag))
                {
                    return pooledİtems[i];
                }
            }

            foreach (PoolData item in items)
            {
                if (item.Expandable && item.CollectablePrefab.CompareTag(tag))
                {
                    for (int i = 0; i < item.Amount; i++)
                    {
                        GameObject pooledObj = Instantiate(item.CollectablePrefab);
                        pooledObj.SetActive(false);
                        pooledObj.transform.SetParent(transform);
                        pooledİtems.Add(pooledObj);

                    }
                }

            }
            return null;
        }


    }
}