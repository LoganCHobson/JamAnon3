using SolarStudios;
using UnityEngine;


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
        if (other.CompareTag("Doorway") && other.transform.root != transform.root)
        {
            other.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Player"))
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
                ScoreManager.instance.AddScore(100);
                Debug.Log(gameObject.name + " Says Spawning: " + spawnedRoom.gameObject.name);
                EnableAllChildren(spawnedRoom.transform);
            }
            gameObject.SetActive(false);
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
