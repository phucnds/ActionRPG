using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "HealthEffect", menuName = "AdventureKingdom/Abilities/EffectStategy/HealthEffect", order = 0)]
    public class HealthEffect : EffectStrategy
    {
        [SerializeField] float healthChange = 100;

        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach (var target in data.GetTargets())
            {
                var health = target.GetComponent<Health>();
                if(health)
                {
                    if(healthChange < 0)
                    {
                        health.TakeDamage(data.GetUser(),-healthChange);
                    }
                    else
                    {
                        health.Heal(healthChange);
                    }
                }
            }
            finished();
        }
    }
}