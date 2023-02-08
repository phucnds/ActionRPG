using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] bool isHoming = true;
        [SerializeField] float speed = 1;
        [SerializeField] float acceleration = 2f;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;
        Vector3 targetPoint;

        Health target = null;
        float damage = 0;
        GameObject instigator = null;

        private void Start() {
            transform.LookAt(GetAimLocation());
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (target != null && isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }

            speed += acceleration;
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        }

        public void SetTarget(Health target,GameObject instigator ,float damage)
        {
            SetTarget(instigator, damage, target);
        }

        public void SetTarget(Vector3 targetPoint, GameObject instigator, float damage)
        {
            SetTarget(instigator, damage, null, targetPoint);
        }

        

        public void SetTarget(GameObject instigator, float damage, Health target = null, Vector3 targetPoint = default)
        {
            this.target = target;
            this.damage = damage;
            this.targetPoint = targetPoint;
            this.instigator = instigator;
            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {

            if (target == null)
            {
                return targetPoint;
            }

            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other) {


            Health health = other.GetComponent<Health>();
            if (target != null && health != target) return;
            if (health == null || health.IsDead()) return;
            if (other.gameObject == instigator) return;
            health.TakeDamage(instigator, damage);
            speed = 0;
            
            

            if(hitEffect != null)
            {
                Instantiate(hitEffect,other.transform.position,transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject,lifeAfterImpact);
        }
    }
}


