using SolarStudios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestResetter : MonoBehaviour
{
    public ObjectPool pool;
    public UnityEvent evnt;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(GameObject obj in pool.objectPool)
            {
                if(obj.activeInHierarchy)
                {
                    fixRooms(obj);
                }
            }
            pool.RecycleAll();
            evnt.Invoke();
        }
    }

    void fixRooms(GameObject obj)
    {
         //I know. This is just getting the list of rooms from each pool.
        
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetActive(true);
            }
        
    }
}
