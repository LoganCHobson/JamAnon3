using SolarStudios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonSpawner : MonoBehaviour
{
    private GameObject objectPoolMaster;
    public Transform spawnPoint;
    public float sphereRadius = 2f;

    private bool countDown = false;
    private void Start()
    {
        spawnPoint = gameObject.transform.GetChild(0);
        objectPoolMaster = GameObject.Find("ObjectPoolMaster");

        CheckSpawnerOverlap();
    }

    private void Update()
    {
        if(countDown)
        {
            float count = 1;
            count -= Time.deltaTime;
            if (count <= 0)
            {
                countDown = false;
                gameObject.SetActive(false);

            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Spawning");

            int rand = Random.Range(1, objectPoolMaster.transform.childCount);
            Debug.Log(rand);

            objectPoolMaster.transform.GetChild(rand).GetComponent<ObjectPool>().Spawn(spawnPoint.position);
            countDown = true;
        }
        
    }

    private void CheckSpawnerOverlap()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereRadius);

        foreach (Collider collider in hitColliders)
        {
            DungeonSpawner otherSpawner = collider.GetComponent<DungeonSpawner>();
            if (otherSpawner != null && otherSpawner != this)
            {
                otherSpawner.gameObject.SetActive(false);
                gameObject.SetActive(false);
                Debug.LogWarning("Spawner disabled due to proximity to another spawner.");
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}

