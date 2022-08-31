using Abstract;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    public class StickmanWalkingState : IState
    {
        private readonly StickManAIController _stickManAIController;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private Vector3 _lastPost=Vector3.zero;

        public float WaitingTime;

        public StickmanWalkingState(StickManAIController stickManAIController, Animator animator,
            NavMeshAgent navMeshAgent)
        {
            _stickManAIController = stickManAIController;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }

        public void OnSetup()
        {
            if (Vector3.Distance(_stickManAIController.Target.transform.position,_lastPost)<=0f)
            {
                WaitingTime += Time.deltaTime;
            }
            
            _lastPost = _stickManAIController.Target.transform.position;
            
        }

        public void OnEnter()
        {
            WaitingTime = 0f;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_stickManAIController.Target.transform.position);
            //_animator.SetFloat("Speed", 1f);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
            //_animator.SetFloat("Speed",0f);
            
        }
    }
}