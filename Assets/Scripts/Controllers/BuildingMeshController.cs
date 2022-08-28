using System;
using System.Collections.Generic;
using DG.Tweening;
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
        [SerializeField] List<MeshRenderer> renderer;
        

        #endregion

        #endregion
        
         public void CalculateSaturation()
          {
              Saturation = (float)manager.buildingsData.PayedAmount / manager.buildingsData.BuildingMarketPrice*3f;
              changeSaturation(Saturation);
          }

         private void changeSaturation(float saturation)
         {
             for (int i = 0; i < renderer.Count; i++)
             {
                 var matSaturation = renderer[i].material;
                 matSaturation.DOFloat(saturation, "_Saturation", 0.5f);
             }
         }
    }
}
