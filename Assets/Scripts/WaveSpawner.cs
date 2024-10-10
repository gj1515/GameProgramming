using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float startTime;
    public float endTime;
    public float spawnRate;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", startTime, spawnRate);
    }

    void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
        InvokeRepeating("Spawn", startTime, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
