using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers.BuildingControllers
{
    public class SideObjectMeshController : MonoBehaviour
    {
        #region Self Variables

        #region public Variables

        public float Saturation;

        #endregion

        #region Serialized Variables
        
        [SerializeField] private BuildingManager manager;
        [SerializeField] private List<MeshRenderer> renderer;
        

        #endregion

        #endregion
        
        public void CalculateSaturation()
        {
            Saturation = (float)manager.BuildingsData.SideObject.PayedAmount / manager.BuildingsData.SideObject.BuildingMarketPrice*3f;
            ChangeSaturation(Saturation);
        }

        private void ChangeSaturation(float saturation)
        {
            foreach (var t in renderer)
            {
                var matSaturation = t.material;
                matSaturation.DOFloat(saturation, "_Saturation", 0.5f);
            }
        }

    }
}