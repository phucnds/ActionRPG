using UnityEngine;
using System;
using System.Collections.Generic;

namespace RPG.Abilities
{
    public abstract class FilterStrategy : ScriptableObject
    {
        public abstract IEnumerable<GameObject> Filter(IEnumerable<GameObject> objetToFilter);
    }
}