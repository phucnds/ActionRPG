using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;
using System;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] GameObject AIResponse;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Button skipButton;
        [SerializeField] Button quitButton;
        [SerializeField] Transform choicesRoot;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] TextMeshProUGUI conversantName;

     

        private void Start() {
            playerConversant = GameObject.FindWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            skipButton.onClick.AddListener(() => {playerConversant.Next();});
            quitButton.onClick.AddListener(() => {playerConversant.Quit();});
            UpdateUI();
        }

        private void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());
            if(!playerConversant.IsActive())
            {
                return;
            }
            conversantName.text = playerConversant.GetCurrentAIConversantName();
            AIResponse.SetActive(!playerConversant.IsChoosing());
            choicesRoot.gameObject.SetActive(playerConversant.IsChoosing());

            if(playerConversant.IsChoosing())
            {
                BuildChoiceList();

            }
            else
            {
                AIText.text = playerConversant.GetText();
                skipButton.gameObject.SetActive(playerConversant.HasNext());
                
            }
            
            

        }

        private void BuildChoiceList()
        {
            ClearChoiceButton();
            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choicesRoot);
                choiceInstance.GetComponentInChildren<TextMeshProUGUI>().text = choice.GetText();
                choiceInstance.GetComponentInChildren<Button>().onClick.AddListener(() => {
                    playerConversant.SelectChoice(choice);
                });
            }
        }

        private void ClearChoiceButton()
        {
            foreach (Transform item in choicesRoot)
            {
                Destroy(item.gameObject);
            }
        }

        
    }
}