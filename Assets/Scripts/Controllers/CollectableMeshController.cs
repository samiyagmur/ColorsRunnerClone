using System;
using Enums;
using System.Collections;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Private Veriables

        private SkinnedMeshRenderer _collectableRenderer;
        
        #endregion
        #region Serialized Variables
        
        #endregion

        #region Public Variables
        
        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }
        

        private void GetReferences()
        {
            _collectableRenderer = GetComponent<SkinnedMeshRenderer>();
        }
        
        public void SetCollectableMaterial(ColorType type)
        {
            _collectableRenderer.material = Resources.Load<Material>($"Materials/{type}Mat");
        }
        
        public void OutlineChange(bool isOutlineActive)
        {
            var materialColor = _collectableRenderer.material;
            
            if (isOutlineActive)
            {
                materialColor.DOFloat(0f, "_OutlineSize", 1f);
            }
            else
            {
                materialColor.DOFloat(100f, "_OutlineSize", .1f);
            }
        }

    }
}