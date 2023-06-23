using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    LevelChanger levelChanger;

    void Awake()
    {
        levelChanger = FindObjectOfType<LevelChanger>();
    }

    public void PlayGame()
    {
        levelChanger.FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
