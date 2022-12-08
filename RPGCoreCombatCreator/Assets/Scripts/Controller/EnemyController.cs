using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Utils;

namespace RPG.Commons 
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] float chaseRange = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;
        
        Combat fighter;
        StatusPoints enemyStatusPoints;
        Mover mover;
        GameObject playerAsTarget;

        Vector3 guardLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Start()
        {
            fighter = GetComponent<Combat>();
            enemyStatusPoints = GetComponent<StatusPoints>();
            mover = GetComponent<Mover>();
            playerAsTarget = GameObject.FindWithTag("Player");

            guardLocation = this.transform.position;
        }

        void Update()
        {
            if (enemyStatusPoints.IsDead()) return;

            if (PlayerIsInRange() && fighter.CanAttack(playerAsTarget))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else 
            {
                PatrolBehaviour(); 
            }

            UpdateTimers();
        }

        private void UpdateTimers() 
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private bool PlayerIsInRange() {
            float distanceToPlayer = Vector3.Distance(playerAsTarget.transform.position, this.transform.position);
            return distanceToPlayer < chaseRange;
        }

        private void AttackBehaviour() 
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(playerAsTarget);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardLocation;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoving(nextPosition);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(this.transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        //Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }

}