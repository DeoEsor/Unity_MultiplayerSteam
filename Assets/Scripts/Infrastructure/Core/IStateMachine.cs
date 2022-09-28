using UnityEngine;
using UnityEngine.AI;

namespace Infrastructure.Core
{
    public interface IStateMachine
    {
        NavMeshAgent AIAgent { get; set; }
        Animator Animator { get; set; }
        public void Enter();

        public void HandleInput();

        public void LogicUpdate();

        public void PhysicsUpdate();

        public void Exit();
    }
}