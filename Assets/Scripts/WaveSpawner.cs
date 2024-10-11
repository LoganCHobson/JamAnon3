using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Wave
{
    public float timeBetweenWaves = 5.0f;  // Time before this wave starts
    public float timeBetweenSpawns = 1.0f; // Time between individual enemy spawns in this wave
    public List<GameObject> objectsToSpawn; // List of prefabs to spawn in this wave
}

public class WaveSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<Wave> waves;               // List of waves
    public int numberOfSpawnPoints = 10;   // Number of random spawn points to generate
    public float spawnRange = 20f;         // Range for spawn points around the spawner
    public float minimumSpawnDistance = 5f; // Minimum distance between each spawn point

    private List<Vector3> spawnPoints = new List<Vector3>();
    private int currentWaveIndex = 0;
    private float waveTimer = 0f;
    private float spawnTimer = 0f;
    private int index = 0;
    private bool isWaveActive = false;

    public List<GameObject> objectsSpawned;
    private Vector3 lastSpawnPosition;

    void Update()
    {
        if (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];

            if (!isWaveActive)
            {
                waveTimer += Time.deltaTime;
                if (waveTimer >= currentWave.timeBetweenWaves)
                {
                    isWaveActive = true;
                    waveTimer = 0f;
                }
            }
            else
            {
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= currentWave.timeBetweenSpawns && index < currentWave.objectsToSpawn.Count)
                {
                    if (index < spawnPoints.Count)  
                    {
                        Vector3 spawnPosition = spawnPoints[index];  

                        GameObject spawnedObject = Instantiate(currentWave.objectsToSpawn[index], spawnPosition, Quaternion.identity);
                        objectsSpawned.Add(spawnedObject);

                        lastSpawnPosition = spawnPosition;

                        index++;
                        spawnTimer = 0f;
                    }
                }

                if (index >= currentWave.objectsToSpawn.Count)
                {
                    isWaveActive = false;
                    index = 0;
                    currentWaveIndex++;
                }
            }
        }
    }

    void GenerateRandomNavMeshPoints()
    {
        spawnPoints.Clear();

        for (int i = 0; i < numberOfSpawnPoints; i++)
        {
            Vector3 newPoint = GetRandomPointOnNavMesh();

            int attempts = 0;
            while (Spacing(newPoint) && attempts < 30)
            {
                newPoint = GetRandomPointOnNavMesh();
                attempts++;
            }

            if (newPoint != Vector3.zero)
            {
                spawnPoints.Add(newPoint);
            }
        }
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }

    bool Spacing(Vector3 newPoint)
    {
        foreach (Vector3 point in spawnPoints)
        {
            if (Vector3.Distance(newPoint, point) < minimumSpawnDistance)
            {
                return true;
            }
        }
        return false;
    }

    public void Restart()
    {
        currentWaveIndex = 0;
        index = 0;

        waveTimer = 0f;
        spawnTimer = 0f;
        isWaveActive = false;

        objectsSpawned.Clear();
        lastSpawnPosition = Vector3.zero;
        GenerateRandomNavMeshPoints();
    }

    /*private void OnEnable()
    {
        Restart();
    }*/

    private void OnDisable()
    {
        foreach (GameObject spawnedObject in objectsSpawned)
        {
            Destroy(spawnedObject);
        }
    }
}
