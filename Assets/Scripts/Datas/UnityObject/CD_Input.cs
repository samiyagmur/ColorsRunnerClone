using UnityEngine;
using Datas.ValueObject;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Input", menuName = "Input/CD_Input", order = 0)]
    public class CD_Input : ScriptableObject
    {
        public InputData InputData;
    }
}