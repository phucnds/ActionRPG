using System;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "DemoTargeting", menuName = "AdventureKingdom/Abilities/TargetingStrategy/DemoTargeting", order = 0)]
    public class DemoTargeting : TargetingStrategy
    {
        public override void StartTargeting(AbilityData data, Action finished)
        {
            finished();
        }
    }
}