using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootEnemies : MonoBehaviour
{

    public List<GameObject> loot = new List<GameObject>();
    // Start is called before the first frame update
   public void SpawnLoot()
    {
        int rand = Random.Range(0, loot.Count);

        Instantiate(loot[rand]);
    }
}
