using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public List<GameObject> Doors = new List<GameObject>();
    GameManager gameManager = GameManager.instance;

    public void ResetRoom()
    {
        foreach(Transform trans in gameObject.transform)
        {
            trans.GetComponent<DungeonSpawner>().active = true;
        }
    }
}
