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

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if all object pools are empty
            if (AreAllPoolsEmpty())
            {
                int rand;
                GameObject spawnedRoom = null;

                // Keep trying to spawn a room until it's not null
                do
                {
                    rand = Random.Range(0, 5);
                    spawnedRoom = objectPoolMaster.transform.GetChild(rand).GetComponent<ObjectPool>().Spawn(spawnPoint.position);
                } while (spawnedRoom == null);

                spawnedRoom.GetComponent<Reseter>().OnEnableAll();
                // If the room is spawned, disable this game object
                gameObject.SetActive(false);
            }
            else
            {
                // Spawn endcap if all pools are empty
                objectPoolMaster.transform.GetChild(5).GetComponent<ObjectPool>().Spawn(endcapSpawnPoint.position, endcapSpawnPoint.rotation);
                gameObject.SetActive(false);
            }
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

}
