using SolarStudios;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    public GameObject objectPoolMaster;
    private Transform spawnPoint;
    public float minDistanceBetweenSpawners = 5f;

    public Transform endcapSpawnPoint;

    public GameObject parent;


    private void Start()
    {
        objectPoolMaster = GameObject.Find("ObjectPoolMaster");
        spawnPoint = transform.GetChild(0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !(other.CompareTag("Doorway")) && other.transform.gameObject != parent)
        {
            Debug.Log("Oop, bad interset. " + other.gameObject.name + " Parent: " + other.transform.gameObject.name);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Spawner"))
        {
            Debug.Log("Removing a Spawner");
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Player"))
        {
            Debug.Log("Spawning a room");
            SpawnRoom();

        }
    }

    // Method to check if all object pools are empty
    private bool AreAllPoolsEmpty()
    {
        for (int i = 0; i < 5; i++)
        {
            ObjectPool pool = objectPoolMaster.transform.GetChild(i).GetComponent<ObjectPool>();

            // Assuming ObjectPool has a method to check if it's empty, e.g., IsEmpty()
            if (!pool.IsEmpty())
            {
                return true;  // If any pool is not empty, return false
            }
        }

        return false;  // All pools are empty
    }

    void SpawnRoom()
    {
        
        if (AreAllPoolsEmpty())
        {
            int rand;
            GameObject spawnedRoom = null;
            do
            {
                rand = Random.Range(0, 5);
                spawnedRoom = objectPoolMaster.transform.GetChild(rand).GetComponent<ObjectPool>().Spawn(spawnPoint.position);
            } while (spawnedRoom == null);
            
            gameObject.SetActive(false);
        }
        else
        {
            objectPoolMaster.transform.GetChild(5).GetComponent<ObjectPool>().Spawn(endcapSpawnPoint.position, endcapSpawnPoint.rotation);
            gameObject.SetActive(false);
        }
    }
}



