using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBarScript : MonoBehaviour
{
    public bool freeze;
    bool setup = false;
    bool entered = false;
    PlayerScript pScript;
    LogicScript lScript;
    LightSpawner lSpawner;
    public float movementSpeed;
    public float rotationSpeed;
    int xScale;
    float yScale;
    Vector3 direction;
    Vector3 target = Vector3.zero;
    Camera mainCam;
    Vector3 window;
    // Start is called before the first frame update
    void Start()
    {
        // freeze = true; //remove
        pScript = FindObjectOfType<PlayerScript>();
        lScript = FindObjectOfType<LogicScript>();
        lSpawner = FindObjectOfType<LightSpawner>();

        if (freeze)
        {
            setup = true;
            return;
        }
    
        mainCam = Camera.main;
        window = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.nearClipPlane));
        
        SetupRandomVar();
        SetupLightBar();
        setup = true;
        Debug.Log(target);
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze)
            return;
        gameObject.transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        gameObject.transform.position += direction * movementSpeed * Time.deltaTime;

        if (target.x > 0)
        {
            
            if (transform.position.x >= target.x)
            {
                Destroy(gameObject);
            }
        }
        else if (target.x < 0)
        {
            if (transform.position.x <= target.x)
            {
                Destroy(gameObject);
            }
        }

        if (target.y > 0)
        {
            if (transform.position.y >= target.y)
            {
                Destroy(gameObject);
            }
        }
        else if (target.y < 0)
        {
            if (transform.position.y <= target.y)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && setup && !entered)
        {
            entered = true;
            pScript.collided++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && setup && entered)
        {
            entered = false;
            pScript.collided--;
        }
    }

    void SetupRandomVar()
    {
        movementSpeed = Random.Range(lSpawner.minMoveSpeed, lSpawner.maxMoveSpeed);
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;

        rotationSpeed = 0;
        if (Random.Range(0f, 1f) > 0.6f)
            rotationSpeed = Random.Range(0 , 125);

        xScale = Random.Range(2, 12);
        yScale = Random.Range(0.2f, 2f);
    }

    void SetupLightBar()
    {
        Vector3 pos = mainCam.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), mainCam.nearClipPlane));
        Vector3 point = mainCam.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), mainCam.nearClipPlane));
        pos.z = 0;
        float rotation = transform.localRotation.eulerAngles.z;

        if (Random.Range(0f, 1f) > 0.5f)
        {
            transform.localScale = new Vector3(xScale, yScale, 1f);
            pos.x = (window.x + ((6f + (xScale + 1f) * 2f) * 0.25f)) - ((((xScale) * 2f) * 0.25f) * (rotation / 90f));
            target.x = -pos.x;
            if (Random.Range(0f, 1f) > 0.5f)
            {
                pos.x = -pos.x;
                direction.x = -direction.x;
                target.x = -target.x;
            }
        }
        else
        {
            transform.localScale = new Vector3(yScale, xScale, 1f);
            pos.y = (window.y + ((6f + (xScale + 1f) * 2f) * 0.25f)) - ((((xScale) * 2f) * 0.25f) * (rotation / 90f));
            target.y = -pos.y;
            if (Random.Range(0f, 1f) > 0.5f)
            {
                pos.y = -pos.y;
                direction.y = -direction.y;
                target.y = -target.y;
            }
        }
        direction = (point - pos).normalized;
        direction.z = 0;
        transform.position = pos;
    }
}
