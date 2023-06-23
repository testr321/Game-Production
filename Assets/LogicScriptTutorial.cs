using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScriptTutorial : MonoBehaviour
{
    [SerializeField] GameObject animatedTextPrefab;
    [SerializeField] GameObject canvas;
    [SerializeField] float defaultCooldown;
    [SerializeField] TextMeshProUGUI defaultTextColour;
    [SerializeField] TextMeshProUGUI greenTextColour;
    [SerializeField] TextMeshProUGUI redTextColour;

    public bool start;
    public bool freeze;
    public bool end = true;
    public PlayerScriptTutorial pScript;
    public GameObject lightBarPrefab;
    public float bounceTime;
    public float touchTime;
    public float stayTime;
    public float timer;
    public static float score = 0;
    bool firstTouch = true;

    float cooldown;

    // Update is called once per frame
    void Update()
    {
        if (!start)
            return;
            
        UpdateTexts();
        if (freeze || PauseMenu.gameIsPaused)
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
        // UpdatePlayer();
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
            timer += Time.deltaTime;
            return;
        }
        else if (pScript.collided < 0)
            Debug.LogError("collided count less than 0: " + pScript.collided);

        timer -= stayTime * Time.deltaTime;
    }

    void UpdateTexts()
    {

    }

    public void BorderReduce()
    {
        // timer -= bounceTime;
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
            pScript.gameObject.transform.position = new Vector3(-1.4f, 0.45f, 0f);
            pScript.horizontalForce = 0;
            pScript.verticalForce = 0;
            timer = 10;
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
