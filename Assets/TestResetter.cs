using SolarStudios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResetter : MonoBehaviour
{
    public ObjectPool pool;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pool.RecycleAll();
        }
    }
}
