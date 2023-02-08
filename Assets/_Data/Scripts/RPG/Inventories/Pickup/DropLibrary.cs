using UnityEngine;
using System.Collections.Generic;

namespace RPG.Inventories
{

    [CreateAssetMenu(fileName = "DropLibrary", menuName = "AdventureKingdom/Inventory/DropLibrary", order = 3)]
    public class DropLibrary : ScriptableObject
    {

        [System.Serializable]
        class DropConfig
        {
            public InventoryItem item;
            public float[] relativeChance;
            public int[] minNumber;
            public int[] maxNumber;
            
            public int GetRandomNumber(int level)
            {
                if(!item.IsStackable())
                {
                    return 1;
                }    

                int min = GetByLevel(minNumber,level);
                int max = GetByLevel(maxNumber,level);
                return Random.Range(min,max + 1);
            }
        }

        [SerializeField] DropConfig[] potentialDrops;
        [SerializeField] float[] dropChancePercentage;
        [SerializeField] int[] minDrops;
        [SerializeField] int[] maxDrops;

        


        public struct Dropped
        {
            public InventoryItem item;
            public int number;
        }

        public IEnumerable<Dropped> GetRandomDrops(int level)
        {
           if(!ShouldRandomDrop(level))
           {
                yield break;
           }

            for (int i = 0; i < GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }

        }

        private float GetTotalChance(int level)
        {
            float total = 0;
            foreach (var drop in potentialDrops)
            {
                total += GetByLevel(drop.relativeChance,level);
            }
            return total;
        }

        private DropConfig SelectRandomItem(int level)
        {
            float totalChance = GetTotalChance(level);
            float randomRoll = Random.Range(0,totalChance);
            float chanceTotal = 0;
            foreach (var drop in potentialDrops)
            {
                chanceTotal += GetByLevel(drop.relativeChance,level);
                if(chanceTotal > randomRoll)
                {
                    return drop;
                }
            }
            return null;
        }

        private Dropped GetRandomDrop(int level)
        {
            var drop = SelectRandomItem(level);
            var result = new Dropped();
            result.item = drop.item;
            result.number = drop.GetRandomNumber(level);

            return result;
        }

        private int GetRandomNumberOfDrops(int level)
        {
            int min = GetByLevel(minDrops,level);
            int max = GetByLevel(maxDrops,level);

            return Random.Range(min,max);
        }

        private bool ShouldRandomDrop(int level)
        {
            return Random.Range(0,100) < GetByLevel(dropChancePercentage,level); 
        }

        static T GetByLevel<T>(T[] values, int level)
        {
            if(values.Length == 0)
            {
                return default;
            }

            if(level>values.Length)
            {
                return values[values.Length - 1];
            }

            if( level <= 0)
            {
                return default;
            }
            return values[level - 1];

        }
    }
}