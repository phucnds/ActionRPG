using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using RPG.Stats;
using RPG.Utils;
using UnityEngine;


namespace RPG.Attributes
{
    public class Mana : MonoBehaviour, ISaveable
    {

        [SerializeField] float manaRegenRate = 2;

        LazyValue<float> mana;
       
        private void Awake() {
            mana = new LazyValue<float>(GetMaxMana);
        }

        private void Start() {
            mana.ForceInit();
        }

        private void Update()
        {
            if (mana.value < GetMaxMana())
            {
                mana.value += manaRegenRate * Time.deltaTime;
                if (mana.value > GetMaxMana())
                {
                    mana.value = GetMaxMana();
                }
            }
        }

        public float GetMana()
        {
            return mana.value;
        }

        public float GetMaxMana()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Mana);
        }

        public float GetManaRegenRate()
        {
            return GetComponent<BaseStats>().GetStat(Stat.ManaRegenRate);
        }

        public bool UseMana(float manaToUse)
        {
            if(manaToUse > mana.value)
            {
                return false;
            }
            mana.value -= manaToUse;
            return true;
        }

        public string GetMagicPoint()
        {
            return String.Format("{0:0}/{1:0}", mana.value, GetMaxMana());
        }

        public object CaptureState()
        {
            return mana.value;
        }

        public void RestoreState(object state)
        {
            mana.value = (float)state;
        }
    }

}
