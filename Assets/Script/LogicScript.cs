using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScript : MonoBehaviour
{
    [SerializeField] GameObject animatedTextPrefab;
    [SerializeField] GameObject canvas;
    [SerializeField] float defaultCooldown;
    [SerializeField] TextMeshProUGUI defaultTextColour;
    [SerializeField] TextMeshProUGUI greenTextColour;
    [SerializeField] TextMeshProUGUI redTextColour;

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

    bool red;
    bool green;
    float cooldown;

    // Update is called once per frame
    void Update()
    {
        UpdateTexts();
        if (freeze || PauseMenu.gameIsPaused)
            return;

        red = false;
        green = false;

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
        
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
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
            green = true;
            timer += Time.deltaTime;
            score += Time.deltaTime;
            return;
        }
        else if (pScript.collided < 0)
            Debug.LogError("collided count less than 0: " + pScript.collided);

        red = true;
        timer -= stayTime * Time.deltaTime;
    }

    void UpdateTexts()
    {
        if (red)
            timerText.color = redTextColour.color;
        else
            timerText.color = defaultTextColour.color;

        timerText.text = Mathf.Ceil(timer).ToString();
        if (green)
            scoreText.color = greenTextColour.color;
        else
            scoreText.color = defaultTextColour.color;
        scoreText.text = Mathf.Ceil(score).ToString();
    }

    public void BorderReduce()
    {
        timer -= bounceTime;
        SpawnAnimatedText("-" + bounceTime.ToString());
    }

    public void TouchReduce()
    {
        if (!pScript.invis && firstTouch && pScript.collided > 0)
        {
            Debug.Log("touch reduce");
            timer -= touchTime;
            SpawnAnimatedText("-" + touchTime.ToString());
            firstTouch = false;
        }

        if (pScript.collided == 0 && cooldown <= 0)
        {
            cooldown = defaultCooldown;
            firstTouch = true;
        }
    }

    void SpawnAnimatedText(string value)
    {
        GameObject animatedText = Instantiate(animatedTextPrefab, canvas.transform);
        animatedText.GetComponent<TextMeshProUGUI>().text = value;
    }
}
