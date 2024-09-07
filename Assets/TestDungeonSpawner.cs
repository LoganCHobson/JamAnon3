using SolarStudios;
using UnityEngine;

public class TestDungeonSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public ObjectPool pool;
    public GameObject parent;
    bool fuck;
    private void Start()
    {
        if (pool == null)
        {
            pool = GameObject.FindObjectOfType<ObjectPool>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (pool == null)
        {
            pool = GameObject.FindObjectOfType<ObjectPool>();
        }
        if (!other.CompareTag("Player") && other.transform.parent.parent.gameObject != parent)
        {
            //Debug.Log("Oop, bad interset. " + other.gameObject.name + " Parent: " + other.transform.parent.parent.gameObject.name);
            gameObject.SetActive(false);


        }
        if (other.CompareTag("Doorway"))
        {
            Debug.Log("Removing a doorway");
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("Player") && !fuck)
        {
            Debug.Log("Spawning a room");
            GameObject room = pool.Spawn(spawnPoint.position);
            gameObject.SetActive(false);

        }





    }
}
