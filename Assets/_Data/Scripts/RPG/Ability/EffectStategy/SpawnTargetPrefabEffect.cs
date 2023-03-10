using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "SpawnTargetPrefabEffect", menuName = "AdventureKingdom/Abilities/EffectStategy/SpawnPrefab", order = 1)]
    public class SpawnTargetPrefabEffect : EffectStrategy
    {
        [SerializeField] Transform prefabToSpawn;
        [SerializeField] float destroyDelay = -1;

        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(Effect(data,finished));
        }

        private IEnumerator Effect(AbilityData data, Action finished)
        {
            Transform instance = Instantiate(prefabToSpawn);
            instance.position = data.GetTargetedPoint();

            if(destroyDelay > 0)
            {
                yield return new WaitForSeconds(destroyDelay);
                Destroy(instance.gameObject);
            }

            finished();
        }
    }
}
