using UnityEngine;

namespace Command.IdleLevelCommands
{
    public class IdleLevelLoaderCommand : MonoBehaviour
    {
        public void InitializeIdleLevel(int _IdleLevelID, Transform idleLevelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/IdlePrefabs/IdleLevel {_IdleLevelID}"), idleLevelHolder);
        }
    }
}