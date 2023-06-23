using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public void OnAnimationComplete()
    {
        Destroy(gameObject);
    }
}
