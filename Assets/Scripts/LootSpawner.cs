using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItem
{
    public GameObject itemPrefab;  
    [Range(0f, 1f)] public float spawnProbability;
}

public class LootSpawner : MonoBehaviour
{
    public List<LootItem> lootTable = new List<LootItem>(); 
    private GameManager gameManager = GameManager.instance;



    public void SpawnLoot()
    {
        LootItem selectedLoot = GetRandomLoot();
        if (selectedLoot.itemPrefab != null && selectedLoot != null)
        {
            GameObject obj = Instantiate(selectedLoot.itemPrefab, transform.position, Quaternion.identity);
            obj.GetComponent<Pickup>().permanent = false;
        }
        else
        {
            Debug.Log("No loot spawned.");
        }
    }

    public GameObject SpawnLootReturnGameObj()
    {
        LootItem selectedLoot = GetRandomLoot();
        if (selectedLoot.itemPrefab != null && selectedLoot != null)
        {
            return selectedLoot.itemPrefab;
        }
        else
        {
            Debug.Log("No loot spawned.");
            return null;
        }
    }

    private LootItem GetRandomLoot()
    {
        float totalWeight = 0f;
        
        foreach (LootItem loot in lootTable)
        {
            totalWeight += loot.spawnProbability;
        }

        float randomValue = Random.Range(0f, totalWeight);
        
        float cumulativeWeight = 0f;
        foreach (LootItem loot in lootTable)
        {
            cumulativeWeight += loot.spawnProbability;
            if (randomValue <= cumulativeWeight)
            {
                return loot;
            }
        }
       
        return null;
    }

    private void OnEnable()
    {
        foreach(LootItem loot in lootTable)
        {
            if(loot.itemPrefab == null)
            {
                if(loot.spawnProbability < 0.90)
                {
                    loot.spawnProbability = Mathf.Max(loot.spawnProbability + (0.04f * gameManager.preRunCount), 0);
                }
                else
                {
                    return;
                }
            }
            else
            {
                loot.spawnProbability = Mathf.Max(loot.spawnProbability - (0.01f * gameManager.preRunCount), 0);
            }
            
        }
    }
}
