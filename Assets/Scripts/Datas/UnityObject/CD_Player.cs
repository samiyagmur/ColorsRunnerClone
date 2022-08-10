using Datas.ValueObject;
using UnityEngine;

namespace Datas.UnityObject
{

[CreateAssetMenu(fileName = "CD_Player", menuName = "Player/CD_Player", order = 0)]
public class CD_Player : ScriptableObject
{
    public PlayerData Data;
}
}