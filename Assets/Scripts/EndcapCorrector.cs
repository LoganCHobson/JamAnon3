using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndcapCorrector : MonoBehaviour
{
    private GameObject inital;
    private void OnTriggerEnter(Collider other)
    {
        if(inital == null)
        {
            inital = other.gameObject;
            Debug.Log("Inital gained " + inital.name);
        }
        if(other.CompareTag("Room") && other.gameObject != inital)
        {
            Debug.Log("Deleted myself because of: " +other.gameObject.name);
            gameObject.GetComponentInChildren<Canvas>().enabled = false;
            gameObject.SetActive(false);
        }
    }
}
