using UnityEngine;
using Signals;
using DG.Tweening;
using Enums;
using Controllers;


namespace Managers
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private DOTweenAnimation _obstacleAnim;

        #endregion

        #region Seriliezed Field

        [SerializeField] TurretController turretController;

        TurretAreaType turretAreaType;

        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnObstacleAnimationStart;
            ObstacleSignals.Instance.onEnterTurretArea += OnEnterTurretArea;
            ObstacleSignals.Instance.onExitTurretArea += OnExitTurretArea;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnObstacleAnimationStart;
            ObstacleSignals.Instance.onEnterTurretArea -= OnEnterTurretArea;
            ObstacleSignals.Instance.onExitTurretArea -= OnExitTurretArea;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion  
        private void Awake()
        {
            _obstacleAnim = this.GetComponent<DOTweenAnimation>();
        }

        private void OnObstacleAnimationStart()
        {
           // _obstacleAnim.DOPlay();
        }

        private void OnEnterTurretArea(Transform transformCollectable)
        {
            turretController.EnterTurretArea(transformCollectable);

        }
        private void OnExitTurretArea()
        {
           turretController.ExitTurretArea();
        }

        

    }
}