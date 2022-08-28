using UnityEngine;

namespace Command.LevelCommands
{
    public class LevelLoaderCommand : MonoBehaviour
    {
        public void InitializeLevel(int _levelID, Transform levelHolder)
        {
            Debug.Log("_levelID"+ _levelID);
            Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/level {_levelID}"), levelHolder);
        }
    }
}