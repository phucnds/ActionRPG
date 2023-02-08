using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Inventories;
using RPG.Core;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "AdventureKingdom/Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<Objective> objectives = new List<Objective>();
        [SerializeField] List<Reward> rewards = new List<Reward>(); 

        [Serializable]
        public class Reward
        {
            [Min(1)]
            public int number;
            public InventoryItem item;
        }

        [Serializable]
        public class Objective
        {
            public string reference;
            public string description;
            public bool useCondition = false;
            public Condition condition;
        }

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return objectives;
        }

        public bool HasObjective(string objectiveRef)
        {
            foreach (var objective in objectives)
            {
                if (objective.reference == objectiveRef)
                {
                    return true;
                }
               
            }
            return false;
        }

        public IEnumerable<Reward> GetRewards()
        {
            return rewards;
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if(quest.name == questName)
                {
                    return quest;
                }
            }
            return null;
        }
    }
}