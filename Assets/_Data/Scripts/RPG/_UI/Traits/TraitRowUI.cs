using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Stats;

namespace RPG.UI
{
    public class TraitRowUI : MonoBehaviour
    {
        [SerializeField] Trait trait = Trait.Strength;
        [SerializeField] TextMeshProUGUI traitName;
        [SerializeField] TextMeshProUGUI valueText;
        [SerializeField] Button reduceButton;
        [SerializeField] Button increaseButton;
        


        TraitStore playerTraitStore = null;

        private void Start() {
            playerTraitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();
            reduceButton.onClick.AddListener(() => Allocate(-1));
            increaseButton.onClick.AddListener(() => Allocate(1));
            valueText.text = playerTraitStore.GetPoints(trait).ToString();
        }

        private void Update() {

            reduceButton.interactable = playerTraitStore.CanAssignPoints(trait, -1);
            increaseButton.interactable = playerTraitStore.CanAssignPoints(trait, +1);
            valueText.text = playerTraitStore.GetProPosedPoints(trait).ToString();
        }

        public void Allocate(int points)
        {
            playerTraitStore.AssignPoints(trait, points);
        }
    }

}