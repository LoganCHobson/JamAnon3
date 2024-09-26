using SolarStudios;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform endcapSpawnPoint;
    public bool active = true;
    public LayerMask collisionLayer; 

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
                if (IsSpawnAreaClear())
                {
                    do
                    {
                        int rand = Random.Range(0, GameManager.instance.roomPool.Count - 1);
                        room = GameManager.instance.roomPool[rand].Spawn(spawnPoint.position);

                    } while (room == null);
                    active = false;
                }
            }
            else if (other.gameObject != gameObject && other.transform.parent != gameObject.transform.parent.parent)
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

    private bool IsSpawnAreaClear()
    {
        Collider[] colliders = Physics.OverlapBox(spawnPoint.position, new Vector3(1, 1, 1), Quaternion.identity, collisionLayer);
        Debug.Log("Found: " + colliders.Length);
        return colliders.Length == 0;
    }
}
