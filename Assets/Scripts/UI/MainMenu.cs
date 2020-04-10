using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject LevelListPanel;
    [SerializeField] Object[] levels;

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
    }

    public void ShowLevelList()
    {
        MainMenuPanel.SetActive(false);
        LevelListPanel.SetActive(true);
    }

    public void ShowMainMenu()
    {
        MainMenuPanel.SetActive(true);
        LevelListPanel.SetActive(false);
    }

    public void LevelButton(int num)
    {
        SceneManager.LoadScene(levels[num].name);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
