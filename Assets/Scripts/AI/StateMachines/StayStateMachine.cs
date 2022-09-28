using Core;
using Infrastructure.Core;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AI.StateMachines
{
    public class StayStateMachine : StateMachineBase
    {
        [SerializeField]
        private string animationName = "Move";
        
        public override void Enter()
        {
            Animator.SetBool(animationName, false);
            AIAgent.isStopped = true;
        }

        public override void HandleInput()
        { }

        public override void LogicUpdate()
        { }

        public override void PhysicsUpdate()
        { }

        public override void Exit()
        { }
    }
}