using SolarStudios;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    public List<ObjectPool> roomPools;
    public Transform spawnPoint;
    public Transform endcapSpawnPoint;
    public bool active = true;
    private BoxCollider col;

    private void Start()
    {
        active = true;
        col.GetComponent<BoxCollider>();
        if (spawnPoint == null)
        {
            Transform trans = transform.Find("SpawnPoint");
            spawnPoint = trans;
        }

        if (endcapSpawnPoint == null)
        {
            endcapSpawnPoint = transform.Find("EndcapSpawnPoint").transform;
        }

        if (roomPools.Count == 0)
        {
            GameObject poolMaster = GameObject.Find("ObjectPoolMaster");
            foreach (Transform pool in poolMaster.transform)
            {
                roomPools.Add(pool.GetComponent<ObjectPool>());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            Debug.Log(gameObject + " Collided with " +  other);
            GameObject room;
            if (other.CompareTag("Player"))
            {
                do
                {
                    int rand = Random.Range(0, roomPools.Count - 1);
                    room = roomPools[rand].Spawn(spawnPoint.position);
                } while (room == null);
                active = false;
            }
            else if (other.gameObject != gameObject && other.transform.parent != gameObject.transform.parent )
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
