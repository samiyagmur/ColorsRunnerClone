using Abstract;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

namespace Controllers
{
    public class StickmanAlIdleState : IState
    {
        private readonly StickManAIController _stickManAIController;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        public StickmanAlIdleState(StickManAIController stickManAIController, Animator animator,
            NavMeshAgent navMeshAgent)
        {
            _stickManAIController = stickManAIController;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }


        public void OnSetup()
        {
            _stickManAIController.Target = choseOneOfTheNearestPlace(5);
        }

        public AITargetStationController choseOneOfTheNearestPlace(int pickFromNearest)
        {
            return Object.FindObjectsOfType<AITargetStationController>()
                .OrderBy(t => Vector3.Distance(_stickManAIController.transform.position, t.transform.position))
                .Where(t => t.IsTargetAvailable == true)
                .Take(pickFromNearest)
                .OrderBy(t => Random.Range(0, int.MaxValue))
                .FirstOrDefault();
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}