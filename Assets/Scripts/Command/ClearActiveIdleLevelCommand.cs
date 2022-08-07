using UnityEngine;

namespace Command
{
    public class ClearActiveIdleLevelCommand : MonoBehaviour
    {
        public void ClearActiveIdleLevel(Transform idleLevelHolder)
        {
            Destroy(idleLevelHolder.GetChild(0).gameObject);
        }
    }
}