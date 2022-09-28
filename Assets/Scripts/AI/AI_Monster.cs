﻿using System;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public partial class AIMonster : MonoBehaviour
    {
        private NavMeshAgent _aiAgent;
        private GameObject _player;
        public GameObject panelGaveOver;
        private Animator _animator;

        public Transform[] wayPoints;
        public int currentPatch;

        public AIState aiEnemy;

        private void Start()
        {
            _animator = gameObject.GetComponent<Animator>();
            _aiAgent = gameObject.GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void FixedUpdate()
        {
            switch (aiEnemy)
            {
                case AIState.Patrol:
                {
                    _aiAgent.isStopped = false;
                    _animator.SetBool("Move", true);
                    
                    _aiAgent.SetDestination(wayPoints[currentPatch].transform.position);
                    
                    var patchDist = Vector3.Distance(wayPoints[currentPatch].transform.position, gameObject.transform.position);
                    
                    if (patchDist < 2)
                    {
                        currentPatch++;
                        currentPatch = currentPatch % wayPoints.Length;
                    }

                    break;
                }
                case AIState.Stay:
                    _animator.SetBool("Move", false);
                    _aiAgent.isStopped = true;
                    break;
                case AIState.Chase:
                    _animator.SetBool("Move", true);
                    _aiAgent.SetDestination(_player.transform.position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            var distPlayer = Vector3.Distance(_player.transform.position, gameObject.transform.position);
            
            if (!(distPlayer < 2)) 
                return;
            
            _player.SetActive(false);
            panelGaveOver.SetActive(true);
        }
    }
}