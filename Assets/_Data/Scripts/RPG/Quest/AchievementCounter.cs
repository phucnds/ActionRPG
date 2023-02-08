using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Saving;
using UnityEngine;

namespace RPG.Quests
{
    public class AchievementCounter : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        private Dictionary<string, int> counts = new Dictionary<string, int>();

        public event System.Action onCountChanged;

        public int AddToCount(string token, int amount, bool onlyIfExists = false)
        {
            if (!counts.ContainsKey(token))
            {
                if (onlyIfExists) return 0;
                counts[token] = amount;
                onCountChanged?.Invoke();
                return amount;
            }
            counts[token] += amount;
            onCountChanged?.Invoke();
            return counts[token];
        }

        public int RegisterCounter(string token, bool overwrite = false)
        {
            if (!counts.ContainsKey(token) || overwrite)
            {
                counts[token] = 0;
                onCountChanged?.Invoke();
            }
            return counts[token];
        }

        public int GetCounterValue(string token)
        {
            if (!counts.ContainsKey(token)) return 0;
            return counts[token];
        }

        public object CaptureState()
        {
            return counts;
        }

        public void RestoreState(object state)
        {
            counts = (Dictionary<string, int>)state;
            onCountChanged?.Invoke();
        }

        public bool? Evaluate(EPredicate predicate, string[] parameters)
        {
            if (predicate == EPredicate.HasKilled)
            {
                if (int.TryParse(parameters[1], out int intParameter))
                {
                    RegisterCounter(parameters[0]);
                    return counts[parameters[0]] >= intParameter;
                }
                return false;
            }
            return null;
        }

    }
}