using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = LogicScript.score.ToString("F0");
        LogicScript.score = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
