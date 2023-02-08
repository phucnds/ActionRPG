using RPG.Stats;
using UnityEngine;

namespace RPG.Inventories
{
    [System.Serializable]
    public struct StatModifier
    {
        public Stat Stat;
        public float InitialValue;
        public float IncreasePerLevel;
        public bool IsPercentage;
        [HideInInspector] public int Level;
        [HideInInspector] public ItemRarity Rarity;


        public StatModifier(Stat stat, float initialValue, float increasePerLevel, bool isPercentage, int level, ItemRarity rarity)
        {
            Stat = stat;
            InitialValue = initialValue;
            IncreasePerLevel = increasePerLevel;
            IsPercentage = isPercentage;
            Level = level;
            Rarity = rarity;
        }

        public float Evaluate()
        {
            float actualInitialValue = (int)Rarity * InitialValue;
            float actualIncreasePerLevel = (int)Rarity * IncreasePerLevel;
            return actualInitialValue + actualIncreasePerLevel * Level;
        }
    }
}