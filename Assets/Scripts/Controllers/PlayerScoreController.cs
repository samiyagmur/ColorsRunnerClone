using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Controllers
{

   

    public class PlayerScoreController : MonoBehaviour
    {
        [SerializeField] GameObject meshController;
        [SerializeField] private TextMeshPro scoreText;
        public void OnChangeScorePos()
        {   
            transform.position = meshController.transform.position+new Vector3(0,2,0);

        }

        internal void UpdateScore(int score)
        {   

            
            scoreText.text = score.ToString();
        }
    }
}