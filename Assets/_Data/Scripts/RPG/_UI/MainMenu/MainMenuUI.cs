using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Utils;
using RPG.Saving;
using RPG.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    LazyValue<SavingWrapper> savingWrapper;
    [SerializeField] TMP_InputField newGameNameField;

    private void Awake()
    {
        savingWrapper = new LazyValue<SavingWrapper>(GetSavingWrapper);
    }

    private SavingWrapper GetSavingWrapper()
    {
        return FindObjectOfType<SavingWrapper>();
    }

    public void ContinueGame()
    {
        savingWrapper.value.ContinueGame();
    }

    public void NewGame()
    {
        savingWrapper.value.NewGame(newGameNameField.text);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
