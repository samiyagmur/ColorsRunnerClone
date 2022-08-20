using System;
using Controllers;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
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

        private MeshRenderer _gateRenderer;
        private MeshRenderer _gateGradient;
        
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
            SendGateTypeToDroneManager(colorType);
        }

        private void GetReferences()
        {
            _gateRenderer = GetComponent<MeshRenderer>();
            _gateGradient = GetComponent<MeshRenderer>();
        }

        private void SetGateMaterial(ColorType type)
        {
            _gateGradient = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
            _gateRenderer.material = Resources.Load<Material>($"Materials/{type}Mat");
            _gateGradient.material = Resources.Load<Material>($"Materials/Gate{type}Mat");
        }

        private void SendGateTypeToDroneManager(ColorType gateColor)
        {
            DroneAreaSignals.Instance.onSetCorrectColorToGround?.Invoke(gateColor);
        }
        
    }
}
