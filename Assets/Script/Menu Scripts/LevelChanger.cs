using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public static bool isChangingScene;

    [SerializeField] Animator animator;

    int levelToLoad;

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("Fade Out");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void StartFade()
    {
        isChangingScene = true;
    }

    public void EndFade()
    {
        isChangingScene = false;
    }
}
