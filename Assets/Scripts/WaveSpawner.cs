using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn; //List of game objects to spawn
    public float timeBetweenSpawns = 1f; //Delay between spawns
    public float timeBetweenWaves = 5f; //Delay between waves
    public int totalWaves = 3; //Total number of waves

    public int wantedEnemyCount;
    public int currentEnemyCount;
    private float nextSpawnTime; //Time to spawn next object
    private int currentWave = 0; //Current wave index
    private Vector3 spawnPos;

    public List<GameObject> lootToSpawn = new List<GameObject>();
    void Start()
    {
        //Start spawning waves
        SpawnWave();

        //SpawnLoot();
    }

    void Update()
    {
        //Check if it's time to spawn the next object
        if (currentEnemyCount <= wantedEnemyCount)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnObject();
                nextSpawnTime = Time.time + timeBetweenSpawns;
            }
        }

        
    }

    private void SpawnLoot()
    {
        foreach(GameObject obj in lootToSpawn)
        {
            float randomX = Random.Range(transform.position.x, transform.position.x - 65);
            float randomZ = Random.Range(transform.position.z, transform.position.z + 65);
            spawnPos = new Vector3(randomX, 20f, randomZ);
            Instantiate(obj, spawnPos, Quaternion.identity);
        }
         
    }

    void SpawnObject()
    {
        //Spawn the next object in the list
        foreach (GameObject obj in objectsToSpawn)
        {
            GameObject objToSpawn = obj;
            float randomX = Random.Range(transform.position.x, transform.position.x - 65);
            float randomZ = Random.Range(transform.position.z, transform.position.z + 65);
            spawnPos = new Vector3(randomX, 5f, randomZ);

            Instantiate(objToSpawn, spawnPos, Quaternion.identity);
            currentEnemyCount += 1;
        }
    }

    void SpawnWave()
    {
        //Check if we've completed all waves
        if (currentWave >= totalWaves)
        {
            Debug.Log("Winning!");
            return;
        }

        //Increment the current wave index
        currentWave++;

        //Send the next wave
        Invoke("SpawnWave", timeBetweenWaves);
    }

    void SpawnerReset()
    {
        currentWave = 0;
    }
    
}
