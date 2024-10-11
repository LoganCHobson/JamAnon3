using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public List<GameObject> temporaryObjs = new List<GameObject>();
    public GameObject loot;
    public GameObject enemy;

    public int howMuchLoot;
    public int howMuchEnemies;

   
    private void OnDisable()
    {
        foreach (GameObject obj in temporaryObjs)
        {
            Destroy(obj);
        }
        temporaryObjs.Clear();

        loot.GetComponent<WaveSpawner>().waves[0].objectsToSpawn.Clear();
        enemy.GetComponent<WaveSpawner>().waves[0].objectsToSpawn.Clear();
        loot.gameObject.SetActive(false);
        enemy.gameObject.SetActive(false);
    }
    private void Start()
    {
        
    }

    private void OnEnable()
    {

        WaveSpawner waveSpawnerLoot = loot.GetComponent<WaveSpawner>();
        LootSpawner lootSpawner = loot.GetComponent<LootSpawner>();


        if (waveSpawnerLoot.waves.Count == 0)
        {
            Wave wave = new Wave();
            wave.timeBetweenSpawns = 0f;
            wave.timeBetweenWaves = 2f;
            for (int i = 0; i < howMuchLoot; i++)
            {
                GameObject lootObj = lootSpawner.SpawnLootReturnGameObj(); 
                if (lootObj != null)
                {
                    wave.objectsToSpawn.Add(lootObj);
                }
            }
            waveSpawnerLoot.waves.Add(wave); 
        }
        else
        {
            foreach (Wave wave in waveSpawnerLoot.waves)
            {
                while (wave.objectsToSpawn.Count < howMuchLoot) 
                {
                    GameObject lootObj = lootSpawner.SpawnLootReturnGameObj();
                    if (lootObj != null)
                    {
                        wave.objectsToSpawn.Add(lootObj);
                    }
                }
            }
        }

        WaveSpawner waveSpawnerEnemy = enemy.GetComponent<WaveSpawner>();
        LootSpawner enemyLootSpawner = enemy.gameObject.GetComponent<LootSpawner>();

        
        if (waveSpawnerEnemy.waves.Count == 0)
        {
            Wave wave = new Wave();
            wave.timeBetweenSpawns = 0f;
            wave.timeBetweenWaves = 2f;
            for (int i = 0; i < howMuchEnemies; i++)
            {
                GameObject enemyObj = enemyLootSpawner.SpawnLootReturnGameObj(); 
                if (enemyObj != null)
                {
                    wave.objectsToSpawn.Add(enemyObj);
                }
            }
            waveSpawnerEnemy.waves.Add(wave); 
        }
        else
        {
            foreach (Wave wave in waveSpawnerEnemy.waves)
            {
                while (wave.objectsToSpawn.Count < howMuchEnemies) 
                {
                    GameObject enemyObj = enemyLootSpawner.SpawnLootReturnGameObj();
                    if (enemyObj != null)
                    {
                        wave.objectsToSpawn.Add(enemyObj);
                    }
                }
            }
        }

        enemy.GetComponent<WaveSpawner>().Restart(); 
        loot.GetComponent<WaveSpawner>().Restart();
    }
}
