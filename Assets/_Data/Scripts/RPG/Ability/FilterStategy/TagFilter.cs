using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "TagFilter", menuName = "AdventureKingdom/Abilities/FilterStrategy/TagFilter", order = 0)]
    public class TagFilter : FilterStrategy
    {
        [SerializeField] string tagToFilter = "";

        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (var obj in objectsToFilter)
            {
                if(obj.CompareTag(tagToFilter))
                {
                    yield return obj;
                }
            }
        }
    }

}