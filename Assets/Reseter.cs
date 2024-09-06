using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour
{
    public void OnEnableAll()
    {
        Transform grandParent = gameObject.transform;
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
