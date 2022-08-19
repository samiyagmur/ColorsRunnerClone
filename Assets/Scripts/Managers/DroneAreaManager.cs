using System;
using System.Threading.Tasks;
using Command.ObstacleCommands;
using Controllers;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class DroneAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion
        #region Private Veriables

        private ColorType randomColor;
        private int _wrongColorIndex;
        private int _correctGroundIndex;
        #endregion
    
        #region Serialized Variables

        [SerializeField] private GameObject droneGameObject;
        [SerializeField] private GateController droneAreasGate;
        [SerializeField] private GroundColorCheckController rightGroundColorController;
        [SerializeField] private GroundColorCheckController leftGroundColorController;
        [SerializeField] private BoxCollider droneAreaEnterCollider;
        [SerializeField] private GameObject droneAreaExitCollider;
        [SerializeField] private BoxCollider rightGroundCollider;
        [SerializeField] private BoxCollider leftGroundCollider;

        #endregion

        #endregion
        
        private void Start()
        {
            SetCorrectColorToGround(droneAreasGate.colorType,SetRandomWrongColorIndex(droneAreasGate.colorType),SetRandomRightGround());
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {   
            
            DroneAreaSignals.Instance.onDisableAllColliders += OnDisableAllColliders;
            DroneAreaSignals.Instance.onEnableDroneAreaCollider += OnEnableDroneAreaCollider;
            DroneAreaSignals.Instance.onDisableDroneAreaCollider += OnDisableDroneAreaCollider;
            DroneAreaSignals.Instance.onDisableWrongColorGround += OnDisableWrongColorGround;
            DroneAreaSignals.Instance.onDroneActive += OnDroneActive;

        }

        private void UnsubscribeEvents()
        {
            
            DroneAreaSignals.Instance.onDisableAllColliders -= OnDisableAllColliders;
            DroneAreaSignals.Instance.onEnableDroneAreaCollider -= OnEnableDroneAreaCollider;
            DroneAreaSignals.Instance.onDisableDroneAreaCollider -= OnDisableDroneAreaCollider;
            DroneAreaSignals.Instance.onDisableWrongColorGround -= OnDisableWrongColorGround;
            DroneAreaSignals.Instance.onDroneActive -= OnDroneActive;
            
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        

        private void OnDisableAllColliders()
        {
            droneAreaEnterCollider.enabled = false;
            rightGroundCollider.enabled = false;
            leftGroundCollider.enabled = false;
        }

        private void OnEnableDroneAreaCollider()
        {
           droneAreaExitCollider.gameObject.SetActive(true);
        }

        private void OnDisableDroneAreaCollider()
        {
           droneAreaExitCollider.gameObject.SetActive(false);
        }

        private void OnDisableWrongColorGround()
        {
            if (_correctGroundIndex == 1)
            {
                leftGroundColorController.gameObject.transform.DOScaleZ(0, 1f).OnComplete(() =>
                {
                    leftGroundColorController.gameObject.SetActive(false);
                });
            }
            else
            {   
                Debug.Log(_correctGroundIndex);
                rightGroundColorController.gameObject.transform.DOScaleZ(0, 1f).OnComplete(() =>
                {
                    rightGroundColorController.gameObject.SetActive(false);
                });
            }
        }

        private void OnDroneActive()
        {
            droneGameObject.SetActive(true);
        }

        private void SetCorrectColorToGround(ColorType gateColor,ColorType randomColor,int _correctGateIndex)
        {
            
            if (_correctGateIndex == 0)
            {
                leftGroundColorController.SetGroundMaterial(gateColor);
                rightGroundColorController.SetGroundMaterial(randomColor);

            }
            else
            {
                rightGroundColorController.SetGroundMaterial(gateColor);
                leftGroundColorController.SetGroundMaterial(randomColor);
            }
        }
        
        private ColorType SetRandomWrongColorIndex(ColorType gateColor)
        { 
            _wrongColorIndex = Random.Range(0, Enum.GetValues(typeof(ColorType)).Length);
            
            randomColor = (ColorType)_wrongColorIndex;
            
            if (randomColor == gateColor)
            {
                _wrongColorIndex = (_wrongColorIndex + 1) % Enum.GetValues(typeof(ColorType)).Length;
        
                return randomColor = (ColorType)_wrongColorIndex;
            }
            
            return randomColor = (ColorType)_wrongColorIndex;
           
        }

        private int SetRandomRightGround()
        {
            return _correctGroundIndex = Random.Range(0, 2);
        }
  
    }
}