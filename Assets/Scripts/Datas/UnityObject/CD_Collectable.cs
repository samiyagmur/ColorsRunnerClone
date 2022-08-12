using System.Collections;
using UnityEngine;
using Datas.ValueObject;

namespace Datas.UnityObject
{

    [CreateAssetMenu(fileName ="CD_Collectable",menuName = "Collectable/CD_Collectable",order =0)]
    public class CD_Collectable :ScriptableObject
    {
        public CollectableData collectableData;
    }
}