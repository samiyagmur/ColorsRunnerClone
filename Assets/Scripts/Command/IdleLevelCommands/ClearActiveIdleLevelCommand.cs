using UnityEngine;

namespace Command.IdleLevelCommands
{
    public class ClearActiveIdleLevelCommand : MonoBehaviour
    {
        public void ClearActiveIdleLevel(Transform idleLevelHolder)
        {
            Destroy(idleLevelHolder.GetChild(0).gameObject);
        }
    }
}