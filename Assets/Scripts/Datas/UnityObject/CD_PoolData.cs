using System.Collections;
using UnityEngine;
using ValueObject;

namespace UnityObject
{

    [CreateAssetMenu(menuName ="Data/CD_PoolData",fileName ="CD_PoolData",order =0)]
    public class CD_PoolData : ScriptableObject
    {
        public PoolData poolData; 
    }
}