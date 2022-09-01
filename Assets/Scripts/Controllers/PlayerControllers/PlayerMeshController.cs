using UnityEngine;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private int _totalScore;

        #endregion Private Variables

        #endregion Self Variables

        public void ChangeScale(float muiltiplyAmount)
        {
            if (transform.parent.gameObject.activeInHierarchy == false)
            {
                transform.parent.gameObject.SetActive(true);
            }

            if (transform.parent.localScale.x >= 1 && transform.parent.localScale.y >= 1 && transform.parent.localScale.y >= 1)
                transform.parent.localScale += Vector3.one / (_totalScore * 2) * muiltiplyAmount;

            if (transform.parent.localScale.x <= 1 && transform.parent.localScale.y <= 1 && transform.parent.localScale.y <= 1)
            {
                transform.parent.localScale = Vector3.one;
            }
        }

        public void CalculateSmallerRate(int score)
        {
            if (score > 0)
            {
                _totalScore = score;
            }
        }
    }
}