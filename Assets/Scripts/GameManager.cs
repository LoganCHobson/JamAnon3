using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SolarStudios;

public class GameManager : MonoBehaviour
{
    public List<GameObject> roomPool = new List<GameObject>();
    public GameObject player;

  
    
    private void Start()
    {
        
    }
    
    void ClearRooms()
    {

        foreach (GameObject pool in roomPool)
        {
            pool.GetComponent<ObjectPool>().RecycleAll();
        }
    }
}
