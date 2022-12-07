using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Commons
{
    public class Mover : MonoBehaviour, Action
    {
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;


        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoving(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveToLocation(destination);
        }

        public void MoveToLocation(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void CancelAction()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

    }

}