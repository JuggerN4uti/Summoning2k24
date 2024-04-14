using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button[] LevelButton;
    int unlocked;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void JumpToLevel(int jump)
    {
        SceneManager.LoadScene(jump);
    }

    public void UpdateButtons()
    {
        unlocked = PlayerPrefs.GetInt("Unlocked");
        if (unlocked >= 16)
            unlocked = 16;
        for (int i = 1; i < unlocked; i++)
        {
            LevelButton[i].interactable = true;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
