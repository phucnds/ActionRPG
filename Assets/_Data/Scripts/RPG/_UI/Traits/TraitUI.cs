using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Stats;
using TMPro;

namespace RPG.UI
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unassignedPoints;
        [SerializeField] Button commitButton;

        TraitStore playerTraitStore = null;

        private void Start() {
            playerTraitStore = GameObject.FindWithTag("Player").GetComponent<TraitStore>(); 
            commitButton.onClick.AddListener(playerTraitStore.Commit);
        }

        private void Update() {
            unassignedPoints.text = playerTraitStore.GetUnassignedPoints() + "";
        }
    }

}
