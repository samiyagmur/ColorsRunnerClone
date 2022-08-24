using System;
using UnityEngine;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {
        public void ChangeScale()       
        {
            Debug.Log("Enter");
            if (transform.parent.gameObject.activeInHierarchy==false)
            {
                transform.parent.gameObject.SetActive(true);
            }
            transform.parent.localScale += Vector3.one/8;
        }
    }
}