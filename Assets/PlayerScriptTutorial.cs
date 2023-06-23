using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptTutorial : MonoBehaviour
{
    public bool start;

    public float maxTimer;
    public float minSpeed;
    public float maxSpeed;
    public float maxScale;
    public float maxForce;
    public float moveSpeed;
    public Joystick joystick;
    public LogicScriptTutorial lScript;
    public bool invis;
    public int collided;

    Rigidbody2D rb;
    SpriteRenderer sr;
    public float horizontalForce = 0f;
    public float verticalForce  = 0f;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        color = sr.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gameIsPaused)
            return;

        gameObject.transform.localScale = CalScale();
        moveSpeed = CalSpeed();
        // if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        if (Input.touchCount > 0 || (Input.GetMouseButton(0)))
        {
            color.a = 1f;
            sr.material.color = color;
            invis = false;
        }
        else
        {
            color.a = 0.5f;
            sr.material.color = color;
            invis = true;
        }

        if (!start)
            return;
        
        if (joystick.Horizontal > 0)
        {
            if (horizontalForce < maxForce)
                horizontalForce += joystick.Horizontal * Time.deltaTime;
            else
                horizontalForce = maxForce;
        }
        else if (joystick.Horizontal < 0)
        {
            if (horizontalForce > -maxForce)
                horizontalForce += joystick.Horizontal * Time.deltaTime;
            else
                horizontalForce = -maxForce;
        }

        if (joystick.Vertical > 0)
        {
            if (verticalForce < maxForce)
                verticalForce += joystick.Vertical * Time.deltaTime;
            else
                verticalForce = maxForce;
        }
        else if (joystick.Vertical < 0)
        {
            if (verticalForce > -maxForce)
                verticalForce += joystick.Vertical * Time.deltaTime;
            else
                verticalForce = -maxForce;
        }
        
        rb.position += new Vector2(horizontalForce, verticalForce) * moveSpeed * Time.deltaTime;
        // rb.AddForce(new Vector2(horizontalForce * moveSpeed, verticalForce * moveSpeed) * Time.deltaTime, ForceMode2D.Force);
        // rb.velocity(new Vector2(horizontalForce * moveSpeed, verticalForce * moveSpeed));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Boundary")
        {
            if (col.gameObject.name == "Top" || col.gameObject.name == "Bottom")
            {
                verticalForce = -verticalForce;
            }
            else if (col.gameObject.name == "Left" || col.gameObject.name == "Right")
            {
                horizontalForce = -horizontalForce;
            }

            lScript.BorderReduce();
        }
    }

    Vector3 CalScale()
    {
        if (lScript.timer <= 0f)
            return Vector3.zero;
            
        float scale = lScript.timer * 0.08f;

        if (scale > maxScale)
        {
            scale = maxScale;
        }

        return new Vector3(scale, scale, 1f);
    }

    float CalSpeed()
    {
        float count = gameObject.transform.localScale.x / 0.1f;
        float speed = maxSpeed - (0.1f * count);

        if (speed < minSpeed)
        {
            speed = minSpeed;
        }

        return speed;
    }
}
