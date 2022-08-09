using UnityEngine;

namespace Command.LevelCommands
{
    public class ClearActiveLevelCommand : MonoBehaviour
    {
        public void ClearActiveLevel(Transform levelHolder)
        {
            Destroy(levelHolder.GetChild(0).gameObject);
        }
    }
}