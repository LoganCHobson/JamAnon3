using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> temporaryObjs = new List<GameObject>();

    private void OnDisable()
    {
        foreach (GameObject obj in temporaryObjs)
        {
            Destroy(obj);
        }
        temporaryObjs.Clear();
    }
}
