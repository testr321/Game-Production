using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    LevelChanger levelChanger;
    bool released = false;

    void Awake()
    {
        levelChanger = FindObjectOfType<LevelChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            released = true;
    }


    public void NextScene()
    {
        if (released)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void StartGame()
    {
        if (released)
            SceneManager.LoadScene("Game Scene");
    }

    public void BackToMainMenu()
    {
        if (released)
            SceneManager.LoadScene(0);
    }
}
