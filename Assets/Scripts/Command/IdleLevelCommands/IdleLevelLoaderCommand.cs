using UnityEngine;

namespace Command.IdleLevelCommands
{
    public class IdleLevelLoaderCommand : MonoBehaviour
    {
        public void InitializeIdleLevel(int _levelID, Transform idleLevelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/IdleLevelPrefabs/IdleLevel {_levelID}"), idleLevelHolder);
        }
    }
}