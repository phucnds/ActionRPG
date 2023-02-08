using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "OrientToTargetEffect", menuName = "AdventureKingdom/Abilities/EffectStategy/OrientToTarget", order = 3)]
    public class OrientToTargetEffect : EffectStrategy
    {
        public override void StartEffect(AbilityData data, Action finished)
        {
            data.GetUser().transform.LookAt(data.GetTargetedPoint());
            finished();
        }
    }
}
