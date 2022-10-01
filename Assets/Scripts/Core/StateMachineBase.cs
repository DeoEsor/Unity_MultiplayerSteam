using Infrastructure.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Core
{
    public abstract class StateMachineBase : MonoBehaviour, IStateMachine
    {
        public NavMeshAgent AIAgent { get; set; }
        public Animator Animator { get; set; }
        
        public IStateObject _controllerObject;
        
        [SerializeField]
        private AudioClip stepsSound;

        public abstract void Enter();

        public abstract void HandleInput();

        public abstract void LogicUpdate();
        
        public abstract void PhysicsUpdate();

        public abstract void Exit();
    }
}