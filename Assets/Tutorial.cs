using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorial1;
    [SerializeField] GameObject tutorial2;
    [SerializeField] GameObject tutorial3;
    [SerializeField] GameObject tutorial4;
    [SerializeField] GameObject tutorial5;
    [SerializeField] GameObject rightCollider;
    [SerializeField] PlayerScriptTutorial playerScriptTutorial;
    [SerializeField] LogicScriptTutorial logicScriptTutorial;
    [SerializeField] GameObject lightBar;
    [SerializeField] GameObject continueText;

    bool released;
    public int stage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0 && !Input.GetMouseButton(0))
            released = true;
        
        if ((Input.touchCount > 0 || Input.GetMouseButton(0)) && released)
        {
            Debug.Log("tap");
            released = false;
            if (stage == 0)
            {
                tutorial1.SetActive(false);
                tutorial2.SetActive(true);
                continueText.SetActive(true);
                playerScriptTutorial.first = true;
                stage++;
            }
            else if (stage == 1)
            {
                tutorial2.SetActive(false);
                continueText.SetActive(true);
                tutorial3.SetActive(true);
                lightBar.transform.position = Vector3.zero;
                stage++;
            }
            else if (stage == 2)
            {
                tutorial3.SetActive(false);
                tutorial4.SetActive(true);
                playerScriptTutorial.start = true;
                logicScriptTutorial.start = true;
                rightCollider.SetActive(true);
                continueText.SetActive(false);
            }
            else if (stage == 3)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Stage5()
    {
        tutorial4.SetActive(false);
        tutorial5.SetActive(true);
        continueText.SetActive(true);
        rightCollider.SetActive(false);
        playerScriptTutorial.start = false;
        logicScriptTutorial.start = false;
        stage++;
    }
}
