using Enums;
using System.Collections;
using Managers;
using UnityEngine;


namespace Controlers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Private Veriables

        #endregion
        #region Serialized Variables
        
        #endregion

        #region Public Variables
        
        #endregion

        #endregion


        public void GetCollectableMaterial(ColorType type)
        {
            GetComponent<Renderer>().material = Resources.Load<Material>($"Materials/{type.ToString()}Mat");
        }


    }
}