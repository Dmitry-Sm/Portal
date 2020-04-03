using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject LevelListPanel;

    // private void Start() {
    //     MainMenuPanel = this.transform.GetChild()
    // }

    public void MainMenuToLevelListButton()
    {
        MainMenuPanel.SetActive(false);
        LevelListPanel.SetActive(true);
    }

    public void LevelListToMainMenuButton()
    {
        MainMenuPanel.SetActive(true);
        LevelListPanel.SetActive(false);
    }

    public void LevelButton(int levelNum)
    {
        SceneManager.LoadScene("Level_01");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
