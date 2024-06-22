using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform spawner;
    public Vector3 spawnPos;
    public int enemySpawnCount;

    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    public int amountOfEnemies = 0;



    void Awake()
    {
     

    }
    void Start()
    {
        StartCoroutine(EnemySpawn(enemyPrefab1));
        StartCoroutine(EnemySpawn(enemyPrefab2));
        StartCoroutine(EnemySpawn(enemyPrefab3));

    }

    IEnumerator EnemySpawn(GameObject enemyPrefab)
    {
        spawner = GetComponent<Transform>();
        while (enemySpawnCount <= amountOfEnemies)
        {
            float randomX = Random.Range(spawner.position.x, spawner.position.x - 70);
            float randomZ = Random.Range(spawner.position.z, spawner.position.x + 70);
            spawnPos = new Vector3(randomX, 5f, randomZ);


            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            yield return null;
            enemySpawnCount += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
