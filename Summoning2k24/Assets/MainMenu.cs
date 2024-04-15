using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button[] LevelButton;
    int unlocked;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            PlayerPrefs.SetInt("Unlocked", 1);
    }

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
        for (int i = 1; i < 20; i++)
        {
            LevelButton[i].interactable = false;
        }
        unlocked = PlayerPrefs.GetInt("Unlocked");
        if (unlocked >= 20)
            unlocked = 20;
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
