using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawner : MonoBehaviour
{
    public bool freeze;
    public GameObject lightBarPrefab;
    public float spawnDelay;
    public float minSpawnDelay;
    public float decreaseDelay;
    public float minMoveSpeed;
    public float maxMoveSpeed;
    int minRotation = 0;
    int maxRotation = 91;
    Camera mainCam;
    Vector3 window;
    float nextSpawnTime;
    float nextDecreaseTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
        mainCam = Camera.main;
        window = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.nearClipPlane));
        // lightBarPrefab.SetActive(false);
        // for (int i = 0; i < 5000; i++)
        // {
        //     SpawnLightBar();
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze)
            return;

        if (nextSpawnTime <= Time.time)
        {
            SpawnLightBar();
            nextSpawnTime = Time.time + spawnDelay;
            if (spawnDelay > minSpawnDelay && nextDecreaseTime <= Time.time)
            {
                nextDecreaseTime = Time.time + 2f;
                spawnDelay -= decreaseDelay;
            }
        }
        // Debug.Log(Time.time);
    }

    void SpawnLightBar()
    {
        int rotation = Random.Range(minRotation, maxRotation);
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, rotation);
        // Vector3 pos = mainCam.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), mainCam.nearClipPlane));
        // pos.z = 0;
        
        // if (Random.Range(0f, 1f) > 0.5f)
        // {
        //     pos.x = window.x;
        //     if (Random.Range(0f, 1f) > 0.5f)
        //         pos.x = -pos.x;
        // }
        // else
        // {
        //     pos.y = window.y;
        //     if (Random.Range(0f, 1f) > 0.5f)
        //         pos.y = -pos.y;
        // }
        Instantiate(lightBarPrefab, Vector3.zero, randomRotation);
    }
}
