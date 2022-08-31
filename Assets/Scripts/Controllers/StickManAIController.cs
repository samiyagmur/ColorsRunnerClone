using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Controllers;
using Abstract;
using System;

namespace Controllers
{
    public class StickManAIController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        public Transform Target;
        public Transform RichTarget;
        

        private void Awake()
        {
            _animator=GetComponent<Animator>();
            _navMeshAgent=GetComponent<NavMeshAgent>();
            _stateMachine=new StateMachine();

            var idleState = new StickmanAlIdleState(this, _animator, _navMeshAgent);
            var walkingState = new StickmanWalkingState(this, _animator, _navMeshAgent);
        }


        private void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition); 

        Func<bool> hasTarget() => () => Target!=null && 1<Vector3.Distance(transform.position,Target.position);
        Func<bool> hasRichTargetTarget() => () => RichTarget != null;














    }
}