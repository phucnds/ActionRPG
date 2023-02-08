using System.Collections.Generic;
using RPG.Inventories;
using RPG.Stats;
using UnityEngine;


namespace RPG.Inventories
{
    /*
    [CreateAssetMenu(fileName = "UpgradeableEquipment", menuName = "AdventureKingdom/Inventory/UpgradableEquipment", order = 0)]
    public class UpgradableEquipmentItem : EquipableItem, IModifierProvider
    {
        [SerializeField] private StatModifier[] possiblePrimaryStats;
        [SerializeField] private StatModifier[] possibleSecondaryStats;


        private ItemRarity rarity;
        private ItemQuality quality;
        private int itemLevel;

        public ItemRarity Rarity() => rarity;
        public ItemQuality Quality() => quality;
        public int ItemLevel() => itemLevel;

        private List<StatModifier> actualStatModifiers = new List<StatModifier>();

        /// <summary>
        /// Determine rarity and quality based on the level.  Create stats based on setup values.
        /// </summary>
        /// <param name="level"></param>
        public override void SetupNewItem(int level)
        {
            if (possibleSecondaryStats.Length < 4)
            {
                Debug.Log($"{GetDisplayName()} needs more possible secondary items to function properly!");
            }

            if (possiblePrimaryStats.Length == 0)
            {
                Debug.Log($"{GetDisplayName()} needs at least one possible Primary Stat to function");
            }
            rarity = (ItemRarity)GenerateCharacteristic(level, 1, 5);
            quality = (ItemQuality)GenerateCharacteristic(level, 0, 4);
            StatModifier primaryModifier = possiblePrimaryStats[Random.Range(0, possiblePrimaryStats.Length)];
            actualStatModifiers.Clear();
            actualStatModifiers.Add(primaryModifier);

            for (int i = 0; i < (int)quality; i++)
            {
                bool isValid = true;
                do
                {
                    StatModifier potentialModifier = possibleSecondaryStats[Random.Range(0, possibleSecondaryStats.Length)];
                    foreach (StatModifier actualStatModifier in actualStatModifiers)
                    {
                        if (potentialModifier.Stat == actualStatModifier.Stat &&
                            potentialModifier.IsPercentage == actualStatModifier.IsPercentage)
                        {
                            isValid = false;
                            break;
                        }
                    }
                } while (!isValid);
            }
        }

        private static int GenerateCharacteristic(int level, int startingValue, int maxValue = 5)
        {
            int workingValue = startingValue;
            int testLevel = level * 4;
            // here, we're giving the item a chance to grow past Common. 
            // Each successful roll increases the Rarity, but the test for the next rarity level drops 
            while (workingValue < maxValue && Random.Range(0, 100) < testLevel)
            {
                workingValue++;
                testLevel = level / workingValue;
            }

            return workingValue;
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (var actualStatModifier in actualStatModifiers)
            {
                if (stat == actualStatModifier.Stat && !actualStatModifier.IsPercentage)
                    yield return actualStatModifier.Evaluate();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (var actualStatModifier in actualStatModifiers)
            {
                if (stat == actualStatModifier.Stat && actualStatModifier.IsPercentage)
                    yield return actualStatModifier.Evaluate();
            }
        }

        [System.Serializable]
        struct ModifierSaveData
        {
            public int stat;
            public float initialValue;
            public float increasePerLevel;
            public bool isPercentage;
            public int Level;
        }

        [System.Serializable]
        struct UpgradeSaveData
        {
            public int rarity;
            public int quality;
            public int itemLevel;
            public List<ModifierSaveData> saveData;
        }

        public override object CaptureState()
        {
            UpgradeSaveData state = new UpgradeSaveData
            {
                rarity = (int)rarity,
                quality = (int)quality,
                itemLevel = itemLevel
            };
            List<ModifierSaveData> data = new List<ModifierSaveData>();
            foreach (StatModifier actualStatModifier in actualStatModifiers)
            {
                ModifierSaveData item = new ModifierSaveData
                {
                    stat = (int)actualStatModifier.Stat,
                    initialValue = actualStatModifier.InitialValue,
                    increasePerLevel = actualStatModifier.IncreasePerLevel,
                    isPercentage = actualStatModifier.IsPercentage,
                    Level = actualStatModifier.Level
                };
                data.Add(item);
            }
            state.saveData = data;
            return state;
        }

        public override void RestoreState(object state)
        {
            if (state is UpgradeSaveData upgradeSaveData)
            {
                rarity = (ItemRarity)upgradeSaveData.rarity;
                quality = (ItemQuality)upgradeSaveData.quality;
                itemLevel = upgradeSaveData.itemLevel;
                actualStatModifiers.Clear();
                foreach (ModifierSaveData saveData in upgradeSaveData.saveData)
                {
                    StatModifier modifier = new StatModifier();
                    modifier.Stat = (Stat)saveData.stat;
                    modifier.InitialValue = saveData.initialValue;
                    modifier.IncreasePerLevel = saveData.increasePerLevel;
                    modifier.IsPercentage = saveData.isPercentage;
                    modifier.Level = saveData.Level;
                    actualStatModifiers.Add(modifier);
                }
            }
        }
    }
    */
}