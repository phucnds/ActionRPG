using System.Collections;
using System.Collections.Generic;
using RPG.Quests;
using UnityEngine;

public class InitializeGameManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        if(GameObject.FindWithTag("Core") == null)
        {
            Instantiate(gameManager.Core);
        }

        if(GameObject.FindWithTag("Canvas") == null)
        {
            Instantiate(gameManager.Canvas);
        }
    }
}
