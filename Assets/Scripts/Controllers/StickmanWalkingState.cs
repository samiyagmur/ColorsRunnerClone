using Abstract;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    public class StickmanWalkingState : IState
    {
        private readonly StickManAIController _stickManAIController;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;

        public StickmanWalkingState(StickManAIController stickManAIController, Animator animator, NavMeshAgent navMeshAgent)
        {
            _stickManAIController = stickManAIController;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }

        public void OnSetup()
        {
           
        }
    }
}