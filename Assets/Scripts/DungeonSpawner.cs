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
            Vector3 adjustedPosition = spawnPoint.position; // Default position

            if (other.CompareTag("Player"))
            {
                do
                {
                    int rand = Random.Range(0, GameManager.instance.roomPool.Count - 1);
                    string roomName = GameManager.instance.roomPool[rand].objectPool[0].name;

                    if (roomName.Contains("Level_Void"))
                    {
                        adjustedPosition = spawnPoint.position + new Vector3(0f, -3f, 0f);
                    }
                    else if (roomName.Contains("Level_Basic"))
                    {
                        adjustedPosition = spawnPoint.position + new Vector3(0f, -3f, 0f);
                    }
                    else if (roomName.Contains("Level_Industrial"))
                    {
                        adjustedPosition = spawnPoint.position + new Vector3(0f, -3f, 0f);
                    }
                    else if (roomName.Contains("Level_Towers"))
                    {
                        adjustedPosition = spawnPoint.position + new Vector3(0f, -3f, 0f);
                    }
                    else if (roomName.Contains("Level_Fort"))
                    {
                        adjustedPosition = spawnPoint.position + new Vector3(0f, 1.5f, 0f);
                    }
                    else
                    {
                        adjustedPosition = spawnPoint.position;
                    }

                    room = GameManager.instance.roomPool[rand].Spawn(adjustedPosition);

                } while (room == null);
                active = false;
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

}
