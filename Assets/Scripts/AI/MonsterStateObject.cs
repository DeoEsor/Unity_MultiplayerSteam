using System;
using AI.StateMachines;
using Infrastructure.Core;
using UnityEngine;
using Zenject;

namespace AI
{
    public class MonsterStateObject : MonoBehaviour, IStateObject
    {
        public IStateMachine CurrentState { get; set; }

        [Inject]
        private PatrolStateMachine _patrolStateMachine;
        
        [Inject]
        private StayStateMachine _stayStateMachine;
        
        [Inject]
        private ChaseStateMachine _chaseStateMachine;
        
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

        public void ChangeState(byte newStateFlag) => ChangeState((AIMonster.AIState)newStateFlag);

        public void ChangeState(AIMonster.AIState state)
        {
            switch (state)
            {
                case AIMonster.AIState.Patrol:
                    ChangeState(_patrolStateMachine);
                    break;
                case AIMonster.AIState.Stay:
                    ChangeState(_stayStateMachine);
                    break;
                case AIMonster.AIState.Chase:
                    ChangeState(_chaseStateMachine);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}