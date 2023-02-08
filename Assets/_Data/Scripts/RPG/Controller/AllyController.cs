using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Attributes;
using UnityEngine;

namespace  RPG.Controller
{
    public class AllyController : MonoBehaviour
    {
        [SerializeField] float chaseDictance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.5f;

        Fighter fighter;
        Health health;
        GameObject enemy;
        Mover mover;

       

        Vector3 guardLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Start() {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardLocation = transform.position;

           
        }

        private void GetEnemiesAliveInRange()
        {
            if (enemy != null && !enemy.GetComponent<Health>().IsDead()) return;
            GameObject[] lstEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (lstEnemies.Length <= 0) return;
            foreach (GameObject e in lstEnemies)
            {
                if (e.GetComponent<Health>().IsDead()) continue;
                enemy = e;
  
            }
        }

        private void Update()
        {
            if (health.IsDead()) return;

            GetEnemiesAliveInRange();
            print(enemy.transform.position);
            if (InAttackRangeOfPlayer() && fighter.CanAttack(enemy))
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

            UpdateTimes();
        }

        private void UpdateTimes()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardLocation;

            if(patrolPath != null)
            {
                if(AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            
            if(timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }

        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position,GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(enemy);
        }

        private bool InAttackRangeOfPlayer()
        {
            if(enemy == null) return false;
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, transform.position);
            return distanceToPlayer < chaseDictance;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,chaseDictance);
        }

    }

   
}
