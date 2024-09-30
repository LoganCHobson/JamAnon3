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

    public void SpawnLoot()
    {
        LootItem selectedLoot = GetRandomLoot();
        if (selectedLoot.itemPrefab != null && selectedLoot != null)
        {
            GameObject obj = Instantiate(selectedLoot.itemPrefab, transform);
            obj.GetComponent<Pickup>().permanent = false;
        }
        else
        {
            Debug.Log("No loot spawned.");
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
}
