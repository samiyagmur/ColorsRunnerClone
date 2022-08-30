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

            if (transform.parent.localScale.x >= 1 && transform.parent.localScale.y >= 1 && transform.parent.localScale.y >= 1)
            {
                transform.parent.localScale += Vector3.one / 8 * muiltiplyAmount;//cancelınvoke
            }
        }
    }
}