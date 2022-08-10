using System.Collections;
using UnityEngine;


namespace Controlers
{
    public class CollectableMashController : MonoBehaviour
    {

        public void changeColor(Material material)
        {
            gameObject.GetComponentInParent<Renderer>().material = material;
        }

        public void changeOutLine()
        {

        }
        public void reChangeOutLine()
        {

        }
    }
}