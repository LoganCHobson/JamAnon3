using UnityEngine;
using SolarStudios;

public class DungeonSpawner : MonoBehaviour
{
    private GameObject objectPoolMaster;
    private Transform spawnPoint; // Reference to the spawn point transform
    public float sphereRadius = 2f; // Radius of the sphere cast for overlap check
    public float overlapCheckDelay = 0.1f; // Delay for overlap check
    private float delayCounter; // Counter for the delay
    public int maxSpawnAttempts = 5; // Maximum attempts to spawn a room before giving up
    private int spawnAttempts; // Counter for spawn attempts

    private void Start()
    {
        objectPoolMaster = GameObject.Find("ObjectPoolMaster");

        // Automatically set the spawn point to the first child
        if (transform.childCount > 0)
        {
            spawnPoint = transform.GetChild(0);
        }
        else
        {
            Debug.LogError("DungeonSpawner does not have a child to use as a spawn point.");
        }

        // Initialize the delay counter
        delayCounter = overlapCheckDelay;
    }

    private void Update()
    {
        // Reduce the counter by deltaTime
        if (delayCounter > 0)
        {
            delayCounter -= Time.deltaTime;
            if (delayCounter <= 0)
            {
                // Perform the overlap check once the counter reaches zero
                if (CheckSpawnerOverlap())
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger. Attempting to spawn room...");

            bool roomSpawned = false;
            spawnAttempts = 0;

            // Attempt to spawn a room up to maxSpawnAttempts times
            while (!roomSpawned && spawnAttempts < maxSpawnAttempts)
            {
                spawnAttempts++;

                if (CheckSpawnerOverlap())
                {
                    Debug.Log("Intersection detected. Attempting another spawn...");
                }
                else
                {
                    int rand = Random.Range(0, objectPoolMaster.transform.childCount);
                    Debug.Log("Selected Object Pool Index: " + rand);

                    // Spawn the room using the spawn point position
                    GameObject spawnedRoom = objectPoolMaster.transform.GetChild(rand).GetComponent<ObjectPool>().Spawn(spawnPoint.position);

                    // Start the delay for disabling spawners
                    delayCounter = overlapCheckDelay;

                    // Set this spawner to be deactivated after the delay
                    StartDisableSpawners(spawnedRoom);

                    roomSpawned = true;
                }
            }

            if (!roomSpawned)
            {
                Debug.LogError("Failed to spawn room after " + maxSpawnAttempts + " attempts.");
            }
        }
    }

    private bool CheckSpawnerOverlap()
    {
        // Perform a sphere cast to check for overlapping spawners
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereRadius);

        foreach (Collider collider in hitColliders)
        {
            // Check if the collider belongs to another DungeonSpawner and is not this instance
            DungeonSpawner otherSpawner = collider.GetComponent<DungeonSpawner>();
            if (otherSpawner != null && otherSpawner != this)
            {
                return true; // Intersection detected
            }
        }

        return false; // No intersection detected
    }

    private void StartDisableSpawners(GameObject spawnedRoom)
    {
        // Set this spawner to be deactivated after the delay
        Invoke("DeactivateCurrentSpawner", overlapCheckDelay);

        // Find and deactivate the corresponding spawner in the new room after the delay
        DungeonSpawner[] spawnersInNewRoom = spawnedRoom.GetComponentsInChildren<DungeonSpawner>();
        foreach (DungeonSpawner spawner in spawnersInNewRoom)
        {
            spawner.Invoke("DeactivateIfOverlap", overlapCheckDelay);
        }
    }

    private void DeactivateCurrentSpawner()
    {
        gameObject.SetActive(false);
    }

    private void DeactivateIfOverlap()
    {
        if (CheckSpawnerOverlap())
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the sphere cast in the Scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
