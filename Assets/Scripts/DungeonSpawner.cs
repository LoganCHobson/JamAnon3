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
            if (AreAllPoolsEmpty())
            {
                int rand;
                GameObject spawnedRoom = null;

                do
                {
                    rand = Random.Range(0, 5);
                    spawnedRoom = objectPoolMaster.transform.GetChild(rand).GetComponent<ObjectPool>().Spawn(spawnPoint.position);
                    if(spawnedRoom != null)
                    {
                        FixRoom(spawnedRoom);
                    }
                } while (spawnedRoom == null);

                gameObject.SetActive(false);
            }
            else
            {
                
                objectPoolMaster.transform.GetChild(5).GetComponent<ObjectPool>().Spawn(endcapSpawnPoint.position, endcapSpawnPoint.rotation);
                gameObject.SetActive(false);
            }
        }
    }

   
    private bool AreAllPoolsEmpty()
    {
        for (int i = 0; i < 5; i++)
        {
            ObjectPool pool = objectPoolMaster.transform.GetChild(i).GetComponent<ObjectPool>();

            
            if (!pool.IsEmpty())
            {
                return true;  
            }
        }

        return false; 
    }


    void FixRoom(GameObject obj)
    {
        if (!obj.activeInHierarchy)//All obj that are active
        {
            foreach (Transform child in obj.transform)//All children.
            {
                child.gameObject.SetActive(true);
                foreach (Transform grandchild in child.transform)//All children.
                {
                    Debug.Log("Turned on: " + grandchild.name);
                    grandchild.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            return;
        }
    }
}
