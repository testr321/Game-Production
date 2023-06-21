using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public bool freeze;
    public bool end = true;
    public PlayerScript pScript;
    public GameObject lightBarPrefab;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public float bounceTime;
    public float touchTime;
    public float stayTime;
    public float timer;
    public static float score = 0;
    bool firstTouch = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTexts();
        if (freeze)
            return;

        if (timer <= 0)
        {
            if (!end)
            {
                timer = 0;
                return;
            }
            
            Debug.Log("End Game");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
        UpdatePlayer();
        TouchReduce();
    }

    void UpdatePlayer()
    {
        if (pScript.invis)
        {
            timer -= Time.deltaTime;
            return;
        }

        if (pScript.collided == 0)
        {
            timer += Time.deltaTime;
            score += Time.deltaTime;
            return;
        }
        else if (pScript.collided < 0)
            Debug.Log("collided count less than 0: " + pScript.collided);

        timer -= stayTime * Time.deltaTime;
    }

    void UpdateTexts()
    {
        if (timer < 0)
            timerText.text = "0";
        else
            timerText.text = timer.ToString("F0");
        scoreText.text = score.ToString("F0");
    }

    public void BorderReduce()
    {
        timer -= bounceTime;
    }

    public void TouchReduce()
    {
        if (!pScript.invis && firstTouch && pScript.collided > 0)
        {
            timer -= touchTime;
            firstTouch = false;
        }

        if (pScript.collided == 0)
        {
            firstTouch = true;
        }
    }
}
