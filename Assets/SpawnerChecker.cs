using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<DungeonSpawner>().active = false;
    }
}
