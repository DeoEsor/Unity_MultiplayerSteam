using Core;
using UnityEngine;

namespace AI.StateMachines
{
    public class ChaseStateMachine : StateMachineBase
    {
        [SerializeField]
        private string animationName = "Move";
        
        public GameObject Player { get; set; }
        
        public override void Enter()
        { }
        
        /// <summary>
        /// Use this instead empty enter
        /// </summary>
        /// <param name="player"></param>
        public void Enter(GameObject player)
        {
            Player = player;
            Enter();
        }

        public override void HandleInput()
        { }

        public override void LogicUpdate()
        {
            Animator.SetBool(animationName, true);
            AIAgent.SetDestination(Player.transform.position);
        }

        public override void PhysicsUpdate()
        { }

        public override void Exit()
        { }
    }
}