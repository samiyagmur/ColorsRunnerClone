using UnityEngine;
using Datas.ValueObject;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Stack", menuName = "MENUNAME", order = 0)]
    public class CD_Stack : ScriptableObject
    {
        public StackData Data;
    }
}