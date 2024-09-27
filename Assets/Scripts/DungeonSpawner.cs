using UnityEngine;
using SolarStudios;


public class DungeonSpawner : MonoBehaviour
{
    private GameObject objectPoolMaster;
    private Transform spawnPoint;
    public float minDistanceBetweenSpawners = 5f;

    public Transform endcapSpawnPoint;

    private void Start()
    {
        objectPoolMaster = GameObject.Find("ObjectPoolMaster");

        spawnPoint = transform.GetChild(0);
        CheckSpawnerDistance();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int rand = Random.Range(0, 5);

            GameObject spawnedRoom = objectPoolMaster.transform.GetChild(rand).GetComponent<ObjectPool>().Spawn(spawnPoint.position);
            
            if (spawnedRoom == null)
            {
                GameObject room = objectPoolMaster.transform.GetChild(5).GetComponent<ObjectPool>().Spawn(endcapSpawnPoint.position, endcapSpawnPoint.rotation);
                
                gameObject.SetActive(false);
            }
            else
            {
                EnableAllChildren(spawnedRoom.transform);
                DisableSpawner(spawnedRoom);
            }

            CheckSpawnerDistance();
            gameObject.SetActive(false);
        }
        if(other.CompareTag("Endcap"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponentInChildren<Canvas>().enabled = false;
            gameObject.SetActive(false);
        }
    }


    private void DisableSpawner(GameObject room)
    {

        Transform player = GameObject.Find("Player").transform;
        DungeonSpawner closestSpawner = null;
        float closestDistance = Mathf.Infinity;

        DungeonSpawner[] spawners = room.GetComponentsInChildren<DungeonSpawner>();

        foreach (var spawner in spawners)
        {
            float distance = Vector3.Distance(spawner.transform.position, player.position); 

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSpawner = spawner;
            }
        }

      
        if (closestSpawner != null)
        {
            closestSpawner.gameObject.SetActive(false);
        }
    }

    private void CheckSpawnerDistance()
    {
        DungeonSpawner[] allSpawners = FindObjectsOfType<DungeonSpawner>(); // Find all DungeonSpawner instances

        foreach (DungeonSpawner spawner in allSpawners)
        {
            if (spawner != this) // Skip checking against itself
            {
                float distance = Vector3.Distance(spawner.transform.position, transform.position);

                if (distance <= minDistanceBetweenSpawners)
                {
                    gameObject.SetActive(false);
                    spawner.gameObject.SetActive(false);
                }
            }
        }
    }

    

    void EnableAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(true);
            
            EnableAllChildren(child);
        }
    }
}
