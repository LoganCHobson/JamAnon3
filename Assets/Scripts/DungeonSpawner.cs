using SolarStudios;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform endcapSpawnPoint;
    public bool active = true;


    private void Start()
    {
        active = true;

        if (spawnPoint == null)
        {
            Transform trans = transform.Find("SpawnPoint");
            spawnPoint = trans;
        }

        if (endcapSpawnPoint == null)
        {
            endcapSpawnPoint = transform.Find("EndcapSpawnPoint").transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            Debug.Log(gameObject + " Collided with " + other);
            GameObject room;

            

            if (other.CompareTag("Player"))
            {
                float sphereRadius = 10.0f; // Adjust the radius as needed
                RaycastHit hit;
                if (Physics.SphereCast(spawnPoint.position, sphereRadius, Vector3.up, out hit, 0f)) // The direction can be adjusted
                {
                    // If something is detected, set active to false and return
                    active = false;
                    Debug.Log("Sphere cast detected an object, spawning halted.");
                    return;
                }
                do
                {
                    int rand = Random.Range(0, GameManager.instance.roomPool.Count -1);
                    room = GameManager.instance.roomPool[rand].Spawn(spawnPoint.position);
                } while (room == null);
                active = false;
            }
            else if (other.gameObject != gameObject && other.transform.parent != gameObject.transform.parent)
            {
                if (other.gameObject.TryGetComponent<DungeonSpawner>(out DungeonSpawner spawner))
                {
                    spawner.active = false;
                    active = false;
                }
                else if (other.CompareTag("Endcap"))
                {
                    other.gameObject.SetActive(false);
                }
                else
                {
                    active = false;
                }
            }
        }
    }
}
