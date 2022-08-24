using System;
using UnityEngine;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        public void ChangeScale(float muiltiplyAmount)       
        {
           
            if (transform.parent.gameObject.activeInHierarchy==false)
            {
                transform.parent.gameObject.SetActive(true);
            }
            transform.parent.localScale += Vector3.one/8* muiltiplyAmount;

 
        }
    }
}