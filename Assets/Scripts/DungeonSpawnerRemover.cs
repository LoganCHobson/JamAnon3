using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawnerRemover : MonoBehaviour
{
    private List<GameObject> spawnersInTrigger = new List<GameObject>();
    private GameObject endcapInTrigger = null;

    private void OnTriggerEnter(Collider other)
    {
        // If the object is a spawner
        if (other.gameObject.layer == LayerMask.NameToLayer("Spawner"))
        {
            spawnersInTrigger.Add(other.gameObject);

            // If two spawners are in the trigger, disable both
            if (spawnersInTrigger.Count >= 2)
            {
                DisableSpawners();
            }

            // If a spawner and an endcap are in the trigger, disable both
            if (endcapInTrigger != null)
            {
                DisableSpawnerAndEndcap();
            }
        }

        // If the object is an endcap
        if (other.CompareTag("Endcap"))
        {
            endcapInTrigger = other.gameObject;

            // If a spawner is already in the trigger, disable both
            if (spawnersInTrigger.Count > 0)
            {
                DisableSpawnerAndEndcap();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Remove spawners when they exit the trigger
        if (other.gameObject.layer == LayerMask.NameToLayer("Spawner"))
        {
            spawnersInTrigger.Remove(other.gameObject);
        }

        // Clear the endcap when it exits the trigger
        if (other.CompareTag("Endcap"))
        {
            endcapInTrigger = null;
        }
    }

    // Disable all spawners in the list
    private void DisableSpawners()
    {
        Debug.Log("DisablingSpawner");
        foreach (GameObject spawner in spawnersInTrigger)
        {
            spawner.SetActive(false);
        }
        spawnersInTrigger.Clear();
    }

    // Disable both a spawner and the endcap
    private void DisableSpawnerAndEndcap()
    {
        Debug.Log("Disabling Spawner and endcap");
        foreach (GameObject spawner in spawnersInTrigger)
        {
            spawner.SetActive(false);
        }
        spawnersInTrigger.Clear();

        if (endcapInTrigger != null)
        {
            endcapInTrigger.SetActive(false);
            endcapInTrigger = null;
        }
    }

    //Should fix other issues
    public void OnEnableAll()
    {
        Transform grandParent = transform.parent.parent;
        foreach (Transform obj in grandParent)
        {
            obj.gameObject.SetActive(true);
        }
        EnableAllChildren(grandParent);
    }

    void EnableAllChildren(Transform parent)
    {
        
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(true);
            //Debug.Log("Enabled: " + child);


            EnableAllChildren(child);
        }
    }

}
