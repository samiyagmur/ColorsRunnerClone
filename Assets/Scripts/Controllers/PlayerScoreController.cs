using System.Collections;
using UnityEngine;

namespace Controllers
{

   

    public class PlayerScoreController : MonoBehaviour
    {
        [SerializeField] GameObject meshController;

        public void OnChangeScorePos()
        {   
            transform.position = meshController.transform.position+new Vector3(0,2,0);

        }
    }
}