using System;
using AI.StateMachines;
using Infrastructure.Core;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(ChaseStateMachine), 
        typeof(StayStateMachine), 
        typeof(PatrolStateMachine))]
    public partial class AIMonster : MonoBehaviour, IStateObject
    {
        private NavMeshAgent _aiAgent;
        private GameObject _player;
        private Animator _animator;

        public AIState aiEnemy;
        public IStateMachine CurrentState { get; set; }

        private void Start()
        {
            _animator = gameObject.GetComponent<Animator>();
            _aiAgent = gameObject.GetComponent<NavMeshAgent>();
            Initialize(gameObject.GetComponent<ChaseStateMachine>());
        }

        private void Update() => CurrentState.LogicUpdate();

        private void FixedUpdate()
        {
            var distPlayer = Vector3.Distance(_player.transform.position, transform.position);
            
            if (!(distPlayer < 2)) 
                return;
            
            // TODO Attack logic
        }
        public void Initialize(IStateMachine startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }
        
        public void ChangeState(IStateMachine newState)
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }

        public void ChangeState(AIState state)
        {
            switch (state)
            {
                case AIState.Patrol:
                    ChangeState(new PatrolStateMachine());
                    break;
                case AIState.Stay:
                    ChangeState(new PatrolStateMachine());
                    break;
                case AIState.Chase:
                    ChangeState(new PatrolStateMachine());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            throw new NotImplementedException();
        }
    }
}