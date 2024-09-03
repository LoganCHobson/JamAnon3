using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<GameObject> enemiesToSpawn; 
    public float timeBetweenSpawns = 1.0f; //Time between individual enemy spawns
    public float timeBetweenWaves = 5.0f;  //Time between waves
    public int numberOfWaves = 5;          // Number of waves

    [Header("Spawn Points")]
    public Transform[] spawnPoints;        // Array of spawn points

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < numberOfWaves)
        {
            currentWave++;
            for (int i = 0; i < enemiesToSpawn.Count; i++)
            {
                Vector3 spawnPosition = GetRandomNavMeshPosition();
                if (spawnPosition != Vector3.zero)
                {
                    Instantiate(enemiesToSpawn[i], spawnPosition, Quaternion.identity);
                }
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    Vector3 GetRandomNavMeshPosition()
    {
        if (spawnPoints.Length == 0) return Vector3.zero;

        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint.position, out hit, 2.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;
    }
}
