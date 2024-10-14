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
        Vector3 spawnPosition = transform.position;
        spawnPosition.x += Random.Range(10f, 40f) * (Random.Range(0, 2) == 0 ? 1 : -1);
        spawnPosition.z += Random.Range(10f, 40f) * (Random.Range(0, 2) == 0 ? 1 : -1);

        Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

        Instantiate(prefab, spawnPosition, spawnRotation);
        InvokeRepeating("Spawn", startTime, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
