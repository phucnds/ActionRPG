using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement{
    public class Mover : MonoBehaviour,IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxPathLength = 100f;

        NavMeshAgent agent;
        Health health;

        private void Awake() {
            agent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        private void Start() {
           
        }
        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            var animator = GetComponent<Animator>();
            animator.SetFloat("forwardSpeed", speed);

        }

        public void StartMoveAction(Vector3 destination, float speedFraction){
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination,speedFraction);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxPathLength) return false;
            
            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;

            if (path.corners.Length < 2) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return total;
        }

        public void MoveTo(Vector3 destination,float speedFraction)
        {
            agent.SetDestination(destination);
            agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            agent.isStopped = false;
        }

        public void Cancel(){
            agent.isStopped = true;
        }

        [Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;
            agent.enabled = false;
            transform.position    = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            agent.enabled = true;

            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        
    }

}