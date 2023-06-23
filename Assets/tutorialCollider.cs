using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialCollider : MonoBehaviour
{
    Tutorial tutorial;

    void Awake()
    {
        tutorial = FindObjectOfType<Tutorial>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            tutorial.Stage5();
        }
    }
}
