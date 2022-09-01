using TMPro;
using UnityEngine;

namespace Controllers
{
    public class PlayerScoreController : MonoBehaviour
    {
        #region Self Variables

        #region SerializeField Variables
        [SerializeField] private GameObject meshController;
        [SerializeField] private TextMeshPro scoreText; 
        #endregion

        #endregion
        public void OnChangeScorePos() => transform.position = meshController.transform.position + new Vector3(0, 2, 0);

        internal void UpdateScore(int score) => scoreText.text = score.ToString();

    }
}