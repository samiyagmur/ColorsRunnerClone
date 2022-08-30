using System;
using Enums;
using System.Collections;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Private Veriables

        private SkinnedMeshRenderer _collectableRenderer;

        #endregion
        #region SerializeField Variables
        [SerializeField]
        private CollectableManager collectableManager;
        [SerializeField]
        private float dimensions;
        [SerializeField]
        private float smallerTime;
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
            _collectableRenderer.material = Resources.Load<Material>($"Materials/{type}Mat");//Delete RainbowTag
            
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
                materialColor.DOFloat(100f, "_OutlineSize",0);
            }
        }

        public void CompareColorOnTurretArea(GameObject gameObjectOther,ColorType CurrentCollectableColorType)
        {
            if (gameObjectOther.GetComponent<GroundColorCheckController>().colorType != CurrentCollectableColorType)
            {
                collectableManager.SendCollectableTransform();
            }
        }

        public async void ChangeScale()
        {   
            await Task.Delay(1500);
            transform.parent.parent.DOScale(new Vector3(dimensions, dimensions, dimensions), smallerTime);
            await Task.Delay(80);
            collectableManager.DecreaseStack();

        }
    }
}