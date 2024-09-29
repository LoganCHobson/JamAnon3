using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Wave
{
    public float timeBetweenWaves = 5.0f;  //Time before this wave starts
    public float timeBetweenSpawns = 1.0f; //Time between individual enemy spawns in this wave
    public List<GameObject> enemiesToSpawn; //List of enemy prefabs to spawn in this wave
}

public class WaveSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<Wave> waves;               //List of waves
    public int numberOfSpawnPoints = 10;   //Number of random spawn points to generate

    private List<Vector3> spawnPoints = new List<Vector3>();
    private int currentWaveIndex = 0;

    public List<GameObject> enemiesSpawned;

    void Start()
    {
        GenerateRandomNavMeshPoints();
        StartCoroutine(SpawnWaves());
    }

    void GenerateRandomNavMeshPoints()
    {
        for (int i = 0; i < numberOfSpawnPoints; i++)
        {
            Vector3 randomPoint = GetRandomPointOnNavMesh();
            if (randomPoint != Vector3.zero)
            {
                spawnPoints.Add(randomPoint);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];
            yield return new WaitForSeconds(currentWave.timeBetweenWaves);

            foreach (GameObject enemy in currentWave.enemiesToSpawn)
            {
                Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)];
                 GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
                enemiesSpawned.Add(spawnedEnemy);
                yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
            }

            currentWaveIndex++;
        }
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20.0f; // Adjust the radius as needed
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }
}
