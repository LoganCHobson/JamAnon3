using UnityEngine;
using SolarStudios;

public class DungeonSpawner : MonoBehaviour
{
    private GameObject objectPoolMaster;
    private Transform spawnPoint;
    public float minDistanceBetweenSpawners = 5f;

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
            int rand = Random.Range(0, objectPoolMaster.transform.childCount);

            DisableSpawner(objectPoolMaster.transform.GetChild(rand).GetComponent<ObjectPool>().Spawn(spawnPoint.position));

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

        foreach (var spawner in allSpawners)
        {
            if (spawner != this) // Skip checking against itself
            {
                float distance = Vector3.Distance(spawner.transform.position, transform.position);

                if (distance < minDistanceBetweenSpawners)
                {
                    // Determine which spawner is closer to the player and disable both
                    Transform player = GameObject.Find("Player").transform;
                    float distanceToThis = Vector3.Distance(transform.position, player.position);
                    float distanceToOther = Vector3.Distance(spawner.transform.position, player.position);

                    if (distanceToThis <= distanceToOther)
                    {
                        gameObject.SetActive(false); // Disable this spawner
                    }
                    else
                    {
                        spawner.gameObject.SetActive(false); // Disable the other spawner
                    }
                }
            }
        }
    }
}
