using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class BuildingMeshController : MonoBehaviour
    {
        #region Self Variables

        #region public Variables

        public float Saturation;

        #endregion

        #region Serialized Variables
        
        [SerializeField] private BuildingManager manager;
        

        #endregion

        #endregion
        
        public float CalculateSaturation()
         {
            Saturation = (manager.PayedAmount / manager.BuildingMarketPrice);
            return Saturation;
         }
    }
}
