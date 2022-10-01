using System;
using Core.Core;
using UnityEngine;

namespace AI.StateMachines
{
    [Serializable]
    public class PatrolStateMachine : StateMachineBase
    {
        public Transform[] wayPoints;

        [SerializeField]
        private string animationName = "Move";
        
        public int currentPatch;

        public override void Enter()
        { }

        public override void HandleInput()
        { }

        public override void LogicUpdate()
        {
            AIAgent.isStopped = false;
            Animator.SetBool(animationName, true);
                    
            AIAgent.SetDestination(wayPoints[currentPatch].transform.position);
                    
            var patchDist = Vector3.Distance(wayPoints[currentPatch].transform.position, transform.position);

            if (!(patchDist < 2)) return;
            
            currentPatch++;
            currentPatch %= wayPoints.Length;
        }

        public override void PhysicsUpdate()
        { }

        public override void Exit()
        { }
    }
}