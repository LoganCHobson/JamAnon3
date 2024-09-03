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
                objectPoolMaster.transform.GetChild(5).GetComponent<ObjectPool>().Spawn(endcapSpawnPoint.position, endcapSpawnPoint.rotation);
                gameObject.SetActive(false);
            }
            else
            {
                DisableSpawner(spawnedRoom);
            }
            

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

    private void OnEnable()
    {
        Transform grandParent = transform.parent.parent;
       /* foreach(Transform obj in grandParent)
        {
            obj.gameObject.SetActive(true);
        }*/
        EnableAllChildren(grandParent);
    }

    void EnableAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(true);
            //Debug.Log("Enabled: " + child);


            EnableAllChildren(child);
        }
    }
}
