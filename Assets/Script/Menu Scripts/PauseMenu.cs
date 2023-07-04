using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    
    [SerializeField] Animator animator;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject countDownUI;

    LevelChanger levelChanger;

    void Awake()
    {
        levelChanger = FindObjectOfType<LevelChanger>();
        Resume();
    }

    public void OnResumeButton()
    {
        // pauseMenuUI.SetActive(false);
        // Resume();
    }

    public void OnPauseButton()
    {
        pauseMenuUI.SetActive(true);
        mainMenuUI.SetActive(true);
        countDownUI.SetActive(false);
        Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void PlayResumeAnimation()
    {
        mainMenuUI.SetActive(false);
        // animator.SetTrigger("Resume");
        countDownUI.SetActive(true);
    }

    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void MainMenu()
    {
        Resume();
        LogicScript.score = 0;
        SceneManager.LoadScene(0);
    }
}
