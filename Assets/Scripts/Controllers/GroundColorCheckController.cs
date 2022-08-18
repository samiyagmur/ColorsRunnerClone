using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class GroundColorCheckController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ColorType colorType;

        #endregion
        #region Private Veriables

        private MeshRenderer _groundRenderer;

        #endregion
        

        #endregion
        
        private void Awake()
        {
            GetReferences();
        }

        private void Start()
        {
            //SetGroundMaterial(colorType);
        }

        private void GetReferences()
        {
            _groundRenderer = GetComponent<MeshRenderer>();
        }

        public void SetGroundMaterial(ColorType type)
        {
            colorType = type;
            _groundRenderer.material = Resources.Load<Material>($"Materials/{type}Mat");
        }

    }
}