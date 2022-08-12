using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Signals;
using Controlers;
using System;
using Datas.ValueObject;
using Datas.UnityObject;
using Enums;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Veriables


        #region SerializeField Veriables

        [SerializeField]
        CollectableMeshController collectableMeshController;
        [SerializeField]
        CollectableAnimationController collectableAnimationController;
        [SerializeField]
        CollectableParticalController collectableParticalController;



        [SerializeField]
        private ColorType collectableColorType;

        #endregion

        #region Private Variables

        #endregion

        #region Public Variables
        [Header("Data")]
        public Material Data;


        #endregion

        #endregion

        private void Awake()
        {
            
            Data = GetCollectableMaterial();
           
            
        }
        private void Start()
        {
           GetComponent<Renderer>().material = Data;
        }

        private Material GetCollectableMaterial() => Resources.Load<Material>($"Materials/{collectableColorType.ToString()}Mat");


        #region Event Subcription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onGameOpen += OnGameOpen;
            CoreGameSignals.Instance.onEnterMiniGame += OnEnterMiniGame;
            //CoreGameSignals.Instance.onEnterIdleArea += OnEnterIdleArea;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onGameOpen -= OnGameOpen;
            CoreGameSignals.Instance.onEnterMiniGame = OnEnterMiniGame;
            //CoreGameSignals.Instance.onEnterIdleArea -= OnEnterIdleArea;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        #region Physical Managment
        public void OnIcreaseStack() { StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);}

        public void OnDecreaseStack() 
        { 
            StackSignals.Instance.onDecreaseStack?.Invoke(transform.GetSiblingIndex());
            collectableParticalController.PlayPartical();
        }

        
        private void OnGameOpen() {collectableAnimationController.WhenGameOpen();}
        private void OnPlay() { collectableAnimationController.WhenPlay(); }
        public void OnChangeColor()
        {
           collectableMeshController.
        }
        public void StartPointTurretArea() => collectableAnimationController.WhenEnterTaretArea();
        public void EndPointTaretArea() => collectableAnimationController.WhenExitTaretArea();
        public void StartPointDroneArea(Material materialDrone)
        {
            StackSignals.Instance.onMaterialChangeForDroneArea?.Invoke(materialDrone);//dornkontrol//Materyal kontrol edilcek.
            collectableAnimationController.WhenEnterDronArea();
        }

        public void EndPointDronArea()
        {
            //collectableMashController.reChangeOutLine();
            collectableAnimationController.WhenExitDronArea();
        }

        private void OnEnterMiniGame() { collectableAnimationController.WhenEnterMiniGame(); }
        //private void OnEnterIdleArea() { collectableAnimationController.WhenEnterIdleArea(); }
        private void OnNextLevel() { collectableAnimationController.WhenNextLevel(); }  
      
        //public void onHitBuildingTextArea() => //IdlegameSignals ;

        //public void onHitNextIdleLevel() =>//IdlegameSignals


        #endregion
    }
}