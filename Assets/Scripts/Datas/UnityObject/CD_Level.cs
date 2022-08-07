using UnityEngine;
using Datas.ValueObject;
using System.Collections.Generic;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "ColorsRunner/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> Levels = new List<LevelData>();
    }
}