using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using RPG.Utils;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70;
        [SerializeField] TakeDamageEvent takeDamage;


        [Serializable]
        public class TakeDamageEvent : UnityEvent<float> {}
        public UnityEvent onDie;

        LazyValue<float> healthPoints;

        bool wasDeadLastFrame = false;

        private void Awake() {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private void Start() 
        {
           healthPoints.ForceInit();         
        }

        private void Update() {
            if (healthPoints.value < MaxHP())
            {
                healthPoints.value += GetHPRegenRate() * Time.deltaTime;
                if (healthPoints.value > MaxHP())
                {
                    healthPoints.value = MaxHP();
                }
            }
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetHPRegenRate()
        {
            return GetComponent<BaseStats>().GetStat(Stat.HPRegenRate);
        }
        

        private void OnEnable() {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            
        }
        
        private void OnDisable() {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        public bool IsDead()
        {
            return healthPoints.value <= 0;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            

            if(IsDead())
            {
                onDie.Invoke();
                AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
            UpdateState();
        }

        

        public float GetPercentage()
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health) * 100;
        }

        public void Heal(float healthToRestore)
        {
            healthPoints.value = Mathf.Min(healthPoints.value + healthToRestore, MaxHP());
            UpdateState();
        }

        public string GetHealthPoint()
        {
            return  String.Format("{0:0}/{1:0}",healthPoints.value,GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        public float CurrentHP()
        {
            return healthPoints.value;
        }

        public float MaxHP()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void UpdateState()
        {
            Animator animator = GetComponent<Animator>();
            if (!wasDeadLastFrame && IsDead())
            {
                animator.SetTrigger("die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }

            if (wasDeadLastFrame && !IsDead())
            {
                animator.Rebind();
            }

            wasDeadLastFrame = IsDead();
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience exp = instigator.GetComponent<Experience>();
            if ( exp == null) return;
            exp.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * regenerationPercentage / 100;
            healthPoints.value = Mathf.Max(healthPoints.value,regenHealthPoints);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;
            UpdateState();
            
        }
    }
}
