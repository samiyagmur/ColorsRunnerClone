using System;
using Controllers;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Command.ObstacleCommands
{
    public class GateController : MonoBehaviour
    {
    
    
        #region Self Variables

        #region Public Variables

        public ColorType colorType;

        #endregion
        #region Private Veriables

        private MeshRenderer _GateRender;
        
        #endregion
    
        #region Serialized Variables
        
        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void Start()
        {
            SetGateMaterial(colorType);
        }

        private void GetReferences()
        {
            _GateRender = GetComponent<MeshRenderer>();
        }
        public void SetGateMaterial(ColorType type)
        {
            _GateRender.material = Resources.Load<Material>($"Materials/{type}Mat");
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.CompareTag("Collected"))
            {
                SetCollectablePosition(other.transform.parent.gameObject);
                other.tag = "Collectable";

            }
        }

        private void SetCollectablePosition(GameObject collectable)
        {
            var randomZValue =Random.Range(-(transform.localScale.z/2 - 2),(transform.localScale.z/2 - 2));
            
            Vector3 pos = new Vector3(transform.position.x, transform.position.y,transform.position.z + randomZValue);
            
            collectable.transform.DOMove(pos,2f).OnComplete(() =>
            {
                collectable.GetComponent<CollectableManager>().ChangeAnimationOnController(CollectableAnimType.CrouchIdle);
            });
        }
    }
}
