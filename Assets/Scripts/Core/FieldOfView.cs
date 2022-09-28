﻿using System.Collections;
using AI;
using UnityEngine;
using static UnityEngine.Physics;

namespace Core
{
    public class FieldOfView : MonoBehaviour
    {
        public float radius = 5;
        [Range(0, 360)] public float angle = 60 ;

        public GameObject playerRef;

        public LayerMask targetMask;
        public LayerMask obstacleMask;
        public bool canSeePlayer;

        private void Start()
        {
            // playerRef = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            var wait = new WaitForSeconds(0.2f);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }
        
        private void FixedUpdate()
        {
            gameObject.GetComponent<AIMonster>().aiEnemy = canSeePlayer 
                ? AIMonster.AIState.Chase 
                : AIMonster.AIState.Patrol;
        }

        private void FieldOfViewCheck()
        {
            var rangeChecks = OverlapSphere(transform.position, radius, targetMask); // don't use OverlapSphereNonAlloc method

            if (rangeChecks.Length != 0)
            {
                var target = rangeChecks[0].transform;
                var directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    var distanceToTarget = Vector3.Distance(transform.position, target.position);

                    canSeePlayer = !Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask);
                }
                else
                    canSeePlayer = false;
            }
            else if (canSeePlayer)
                canSeePlayer = false;
        }
    }
}
