using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    PauseMenu pauseMenu;

    void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void OnCountDownComplete()
    {
        pauseMenu.Resume();
    }
}
